using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ITSVoice
{
    public static class HtmlValidationExtensions
    {
        private static StringBuilder BuildItems(string optionLabel, IEnumerable<SelectListItem> selectList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (optionLabel != null)
            {
                stringBuilder.AppendLine(SelectExtensions.ListItemToOption(new SelectListItem()
                {
                    Text = optionLabel,
                    Value = string.Empty,
                    Selected = false
                }));
            }
            foreach (IGrouping<int, SelectListItem> nums in selectList.GroupBy<SelectListItem, int>((SelectListItem i) => {
                if (i.Group == null)
                {
                    return i.GetHashCode();
                }
                return i.Group.GetHashCode();
            }))
            {
                SelectListGroup group = nums.First<SelectListItem>().Group;
                TagBuilder tagBuilder = null;
                if (group != null)
                {
                    tagBuilder = new TagBuilder("optgroup");
                    if (group.Name != null)
                    {
                        tagBuilder.MergeAttribute("label", group.Name);
                    }
                    if (group.Disabled)
                    {
                        tagBuilder.MergeAttribute("disabled", "disabled");
                    }
                    stringBuilder.AppendLine(tagBuilder.ToString(TagRenderMode.StartTag));
                }
                foreach (SelectListItem selectListItem in nums)
                {
                    stringBuilder.AppendLine(SelectExtensions.ListItemToOption(selectListItem));
                }
                if (group == null)
                {
                    continue;
                }
                stringBuilder.AppendLine(tagBuilder.ToString(TagRenderMode.EndTag));
            }
            return stringBuilder;
        }
        private static IEnumerable<SelectListItem> GetSelectListWithDefaultValue(IEnumerable<SelectListItem> selectList, object defaultValue, bool allowMultiple)
        {
            IEnumerable enumerable;
            if (!allowMultiple)
            {
                enumerable = (IEnumerable)(new object[] { defaultValue });
            }
            else
            {
                enumerable = defaultValue as IEnumerable;
                if (enumerable == null || enumerable is string)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "GetSelectListWithDefaultValue", new object[] { "expression" }));
                }
            }
            IEnumerable<string> str =
                from object value in enumerable
                select Convert.ToString(value, CultureInfo.CurrentCulture);
            IEnumerable<string> strs =
                from Enum value in enumerable.OfType<Enum>()
                select value.ToString("d");
            HashSet<string> strs1 = new HashSet<string>(str.Concat<string>(strs), StringComparer.OrdinalIgnoreCase);
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (SelectListItem selectListItem in selectList)
            {
                selectListItem.Selected = (selectListItem.Value != null ? strs1.Contains(selectListItem.Value) : strs1.Contains(selectListItem.Text));
                selectListItems.Add(selectListItem);
            }
            return selectListItems;
        }
        private static IEnumerable<SelectListItem> GetSelectData(this HtmlHelper htmlHelper, string name)
        {
            object obj = null;
            if (htmlHelper.ViewData != null && !string.IsNullOrEmpty(name))
            {
                obj = htmlHelper.ViewData.Eval(name);
            }
            if (obj == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "MvcResources.HtmlHelper_MissingSelectData", new object[] { name, "IEnumerable<SelectListItem>" }));
            }
            IEnumerable<SelectListItem> selectListItems = obj as IEnumerable<SelectListItem>;
            if (selectListItems == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "MvcResources.HtmlHelper_MissingSelectData", new object[] { name, obj.GetType().FullName, "IEnumerable<SelectListItem>" }));
            }
            return selectListItems;
        }
        private static MvcHtmlString SelectInternal(this HtmlHelper htmlHelper, ModelMetadata metadata, string optionLabel, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple, IDictionary<string, object> htmlAttributes)
        {
            ModelState modelState;
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(fullHtmlFieldName))
            {
                throw new ArgumentException("name");
            }
            bool flag = false;
            if (selectList == null)
            {
                selectList = htmlHelper.GetSelectData(name);
                flag = true;
            }
            object model = (allowMultiple ? htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(string[])) : htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(string)));
            if (model == null)
            {
                if (!flag && !string.IsNullOrEmpty(name))
                {
                    model = htmlHelper.ViewData.Eval(name);
                }
                else if (metadata != null)
                {
                    model = metadata.Model;
                }
            }
            if (model != null)
            {
                selectList = GetSelectListWithDefaultValue(selectList, model, allowMultiple);
            }
            StringBuilder stringBuilder = BuildItems(optionLabel, selectList);
            TagBuilder tagBuilder = new TagBuilder("select");
            tagBuilder.InnerHtml = stringBuilder.ToString();
            TagBuilder tagBuilder1 = tagBuilder;
            tagBuilder1.MergeAttributes<string, object>(htmlAttributes);
            tagBuilder1.MergeAttribute("name", fullHtmlFieldName, true);
            tagBuilder1.GenerateId(fullHtmlFieldName);
            if (allowMultiple)
            {
                tagBuilder1.MergeAttribute("multiple", "multiple");
            }
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState) && modelState.Errors.Count > 0)
            {
                tagBuilder1.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }
            tagBuilder1.MergeAttributes<string, object>(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));
            return tagBuilder1.ToMvcHtmlString(0);
        }
        public static MvcHtmlString MyDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string name, IEnumerable<SelectListItem> selectList, string optionLabel = null, object htmlAttributes = null)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            ModelMetadata modelMetadatum = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            return SelectExtensions.DropDownListHelper(htmlHelper, modelMetadatum, name ?? ExpressionHelper.GetExpressionText(expression), selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        public static MvcHtmlString MyTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string name = null, object htmlAttributes = null)
        {
            ModelMetadata modelMetadatum = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            return helper.TextBoxHelper(modelMetadatum, modelMetadatum.Model, name ?? ExpressionHelper.GetExpressionText(expression), null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        internal static object GetModelStateValue(this HtmlHelper htmlHelper, string key, Type destinationType)
        {
            ModelState modelState;
            if (!htmlHelper.ViewData.ModelState.TryGetValue(key, out modelState) || modelState.Value == null)
            {
                return null;
            }
            return modelState.Value.ConvertTo(destinationType, null);
        }
        internal static bool EvalBoolean(this HtmlHelper htmlHelper, string key)
        {
            return Convert.ToBoolean(htmlHelper.ViewData.Eval(key), CultureInfo.InvariantCulture);
        }
        internal static string EvalString(this HtmlHelper htmlHelper, string key, string format)
        {
            return Convert.ToString(htmlHelper.ViewData.Eval(key, format), CultureInfo.CurrentCulture);
        }
        internal static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
        {
            return new MvcHtmlString(tagBuilder.ToString(renderMode));
        }
        private static MvcHtmlString InputHelper(HtmlHelper htmlHelper, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelState modelState;
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(fullHtmlFieldName))
            {
                throw new ArgumentException("MvcResources.Common_NullOrEmpty", "name");
            }
            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes<string, object>(htmlAttributes);
            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(inputType));
            tagBuilder.MergeAttribute("name", fullHtmlFieldName, true);
            string str = htmlHelper.FormatValue(value, format);
            bool flag = false;
            switch (inputType)
            {
                case InputType.CheckBox:
                    {
                        bool? modelStateValue = (bool?)(htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(bool)) as bool?);
                        if (!modelStateValue.HasValue)
                        {
                            goto case InputType.Radio;
                        }
                        isChecked = modelStateValue.Value;
                        flag = true;
                        goto case InputType.Radio;
                    }
                case InputType.Hidden:
                    {
                        string modelStateValue1 = (string)htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(string));
                        tagBuilder.MergeAttribute("value", modelStateValue1 ?? (useViewData ? EvalString(htmlHelper, fullHtmlFieldName, format) : str), isExplicitValue);
                        break;
                    }
                case InputType.Password:
                    {
                        if (value == null)
                        {
                            break;
                        }
                        tagBuilder.MergeAttribute("value", str, isExplicitValue);
                        break;
                    }
                case InputType.Radio:
                    {
                        if (!flag)
                        {
                            string str1 = htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(string)) as string;
                            if (str1 != null)
                            {
                                isChecked = string.Equals(str1, str, StringComparison.Ordinal);
                                flag = true;
                            }
                        }
                        if (!flag & useViewData)
                        {
                            isChecked = htmlHelper.EvalBoolean(fullHtmlFieldName);
                        }
                        if (isChecked)
                        {
                            tagBuilder.MergeAttribute("checked", "checked");
                        }
                        tagBuilder.MergeAttribute("value", str, isExplicitValue);
                        break;
                    }
                default:
                    {
                        goto case InputType.Hidden;
                    }
            }
            if (setId)
            {
                tagBuilder.GenerateId(fullHtmlFieldName);
            }
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState) && modelState.Errors.Count > 0)
            {
                tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }
            tagBuilder.MergeAttributes<string, object>(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));
            if (inputType != InputType.CheckBox)
            {
                return tagBuilder.ToMvcHtmlString(TagRenderMode.SelfClosing);
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(tagBuilder.ToString(TagRenderMode.SelfClosing));
            TagBuilder tagBuilder1 = new TagBuilder("input");
            tagBuilder1.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Hidden));
            tagBuilder1.MergeAttribute("name", fullHtmlFieldName);
            tagBuilder1.MergeAttribute("value", "false");
            stringBuilder.Append(tagBuilder1.ToString(TagRenderMode.SelfClosing));
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
        private static MvcHtmlString TextBoxHelper(this HtmlHelper htmlHelper, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper(htmlHelper, InputType.Text, metadata, expression, model, false, false, true, true, format, htmlAttributes);
        }
    }

    public static class SelectExtensions
    {
        public static MvcHtmlString DropDownListHelper(HtmlHelper htmlHelper, ModelMetadata metadata, string expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.SelectInternal(metadata, optionLabel, expression, selectList, false, htmlAttributes);
        }
        internal static string ListItemToOption(SelectListItem item)
        {
            TagBuilder tagBuilder = new TagBuilder("option");
            tagBuilder.InnerHtml = HttpUtility.HtmlEncode(item.Text);
            TagBuilder value = tagBuilder;
            if (item.Value != null)
            {
                value.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                value.Attributes["selected"] = "selected";
            }
            if (item.Disabled)
            {
                value.Attributes["disabled"] = "disabled";
            }
            return value.ToString(0);
        }
        private static StringBuilder BuildItems(string optionLabel, IEnumerable<SelectListItem> selectList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (optionLabel != null)
            {
                stringBuilder.AppendLine(SelectExtensions.ListItemToOption(new SelectListItem()
                {
                    Text = optionLabel,
                    Value = string.Empty,
                    Selected = false
                }));
            }
            foreach (IGrouping<int, SelectListItem> nums in selectList.GroupBy<SelectListItem, int>((SelectListItem i) => {
                if (i.Group == null)
                {
                    return i.GetHashCode();
                }
                return i.Group.GetHashCode();
            }))
            {
                SelectListGroup group = nums.First<SelectListItem>().Group;
                TagBuilder tagBuilder = null;
                if (group != null)
                {
                    tagBuilder = new TagBuilder("optgroup");
                    if (group.Name != null)
                    {
                        tagBuilder.MergeAttribute("label", group.Name);
                    }
                    if (group.Disabled)
                    {
                        tagBuilder.MergeAttribute("disabled", "disabled");
                    }
                    stringBuilder.AppendLine(tagBuilder.ToString(TagRenderMode.StartTag));
                }
                foreach (SelectListItem selectListItem in nums)
                {
                    stringBuilder.AppendLine(SelectExtensions.ListItemToOption(selectListItem));
                }
                if (group == null)
                {
                    continue;
                }
                stringBuilder.AppendLine(tagBuilder.ToString(TagRenderMode.EndTag));
            }
            return stringBuilder;
        }
        private static IEnumerable<SelectListItem> GetSelectData(this HtmlHelper htmlHelper, string name)
        {
            object obj = null;
            if (htmlHelper.ViewData != null && !string.IsNullOrEmpty(name))
            {
                obj = htmlHelper.ViewData.Eval(name);
            }
            if (obj == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "MvcResources.HtmlHelper_MissingSelectData", new object[] { name, "IEnumerable<SelectListItem>" }));
            }
            IEnumerable<SelectListItem> selectListItems = obj as IEnumerable<SelectListItem>;
            if (selectListItems == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "MvcResources.HtmlHelper_WrongSelectDataType", new object[] { name, obj.GetType().FullName, "IEnumerable<SelectListItem>" }));
            }
            return selectListItems;
        }
        private static IEnumerable<SelectListItem> GetSelectListWithDefaultValue(IEnumerable<SelectListItem> selectList, object defaultValue, bool allowMultiple)
        {
            IEnumerable enumerable;
            if (!allowMultiple)
            {
                enumerable = (IEnumerable)(new object[] { defaultValue });
            }
            else
            {
                enumerable = defaultValue as IEnumerable;
                if (enumerable == null || enumerable is string)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "MvcResources.HtmlHelper_SelectExpressionNotEnumerable", new object[] { "expression" }));
                }
            }
            IEnumerable<string> str =
                from object value in enumerable
                select Convert.ToString(value, CultureInfo.CurrentCulture);
            IEnumerable<string> strs =
                from Enum value in enumerable.OfType<Enum>()
                select value.ToString("d");
            HashSet<string> strs1 = new HashSet<string>(str.Concat<string>(strs), StringComparer.OrdinalIgnoreCase);
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (SelectListItem selectListItem in selectList)
            {
                selectListItem.Selected = (selectListItem.Value != null ? strs1.Contains(selectListItem.Value) : strs1.Contains(selectListItem.Text));
                selectListItems.Add(selectListItem);
            }
            return selectListItems;
        }
        private static MvcHtmlString ListBoxHelper(HtmlHelper htmlHelper, ModelMetadata metadata, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.SelectInternal(metadata, null, name, selectList, true, htmlAttributes);
        }
        private static MvcHtmlString SelectInternal(this HtmlHelper htmlHelper, ModelMetadata metadata, string optionLabel, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple, IDictionary<string, object> htmlAttributes)
        {
            ModelState modelState;
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(fullHtmlFieldName))
            {
                throw new ArgumentException("MvcResources.Common_NullOrEmpty", "name");
            }
            bool flag = false;
            if (selectList == null)
            {
                selectList = htmlHelper.GetSelectData(name);
                flag = true;
            }
            object model = (allowMultiple ? htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(string[])) : htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(string)));
            if (model == null)
            {
                if (!flag && !string.IsNullOrEmpty(name))
                {
                    model = htmlHelper.ViewData.Eval(name);
                }
                else if (metadata != null)
                {
                    model = metadata.Model;
                }
            }
            if (model != null)
            {
                selectList = SelectExtensions.GetSelectListWithDefaultValue(selectList, model, allowMultiple);
            }
            StringBuilder stringBuilder = SelectExtensions.BuildItems(optionLabel, selectList);
            TagBuilder tagBuilder = new TagBuilder("select");
            tagBuilder.InnerHtml = stringBuilder.ToString();
            TagBuilder tagBuilder1 = tagBuilder;
            tagBuilder1.MergeAttributes<string, object>(htmlAttributes);
            tagBuilder1.MergeAttribute("name", fullHtmlFieldName, true);
            tagBuilder1.GenerateId(fullHtmlFieldName);
            if (allowMultiple)
            {
                tagBuilder1.MergeAttribute("multiple", "multiple");
            }
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState) && modelState.Errors.Count > 0)
            {
                tagBuilder1.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }
            tagBuilder1.MergeAttributes<string, object>(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));
            return tagBuilder1.ToMvcHtmlString(0);
        }
    }
}