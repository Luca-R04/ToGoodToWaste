using Core.Domain;

namespace ToGoodToWaste.ViewModels
{
    public class CreatePackageViewModel
    {
        public Package package { get; set; }
        public List<CheckBoxOption> checkBoxes { get; set; }
        public List<int> addedProducts { get; set; }

        public string minTime { get; set; }
        public string maxTime { get; set; }
    }
}
