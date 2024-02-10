using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.View;
using Client.MVVM.View.Town;
using Newtonsoft.Json;
using PropertyChanged;

namespace Client.Services
{
    [AddINotifyPropertyChangedInterface]
    public class UserStore
    {
        public ActivityUser CurrentUser { get; set; }
    }
}
