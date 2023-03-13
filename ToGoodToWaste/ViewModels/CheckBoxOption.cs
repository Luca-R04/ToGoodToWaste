using Core.Domain;

namespace ToGoodToWaste.ViewModels
{
    public class CheckBoxOption
    {
        public bool IsChecked { get; set; }
        public string Description { get; set; }
        public Product product { get; set; }

    }
}
