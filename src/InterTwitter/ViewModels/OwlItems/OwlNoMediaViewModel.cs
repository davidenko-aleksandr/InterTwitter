using InterTwitter.Models;
using System.Windows.Input;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlNoMediaViewModel : OwlViewModel
    {
        public OwlNoMediaViewModel(
            OwlModel model,
            int authorizedUserId,
            ICommand avatarTappedCommand,
            ICommand itemTappedCommand,
            ICommand likeTappedCommad,
            ICommand saveTappedCommand)
            : base(model, authorizedUserId, avatarTappedCommand, itemTappedCommand, likeTappedCommad, saveTappedCommand)
        {
        }
    }
}
