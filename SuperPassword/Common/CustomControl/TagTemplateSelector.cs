using SuperPassword.Shared.DTOs;
using System.Windows;
using System.Windows.Controls;

namespace SuperPassword.Common.CustomControl
{
    public class TagTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TagTemplate { get; set; }
        public DataTemplate NewTagTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var tag = item as TagDTO;
            if (tag != null)
                if (tag.IsNewButton) return NewTagTemplate;
                else return TagTemplate;
            return base.SelectTemplate(item, container);
        }
    }
}
