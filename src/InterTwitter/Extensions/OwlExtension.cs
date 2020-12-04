using InterTwitter.Enums;
using InterTwitter.Models;
using InterTwitter.ViewModels.OwlItems;
using System.Windows.Input;

namespace InterTwitter.Extensions
{
    public static class OwlExtension
    {
        public static OwlViewModel ToViewModel(this OwlModel model, int authorizedUserId, ICommand itemTappedCommand, ICommand likeTappedCommand, ICommand saveTappedCommand)
        {
            OwlViewModel viewModel = null;

            switch (model.MediaType)
            {
                case OwlType.OneImage:
                    {
                        viewModel = new OwlOneImageViewModel(model, authorizedUserId, itemTappedCommand, likeTappedCommand, saveTappedCommand);
                        break;
                    }

                case OwlType.FewImages:
                    {
                        viewModel = new OwlFewImagesViewModel(model, authorizedUserId, itemTappedCommand, likeTappedCommand, saveTappedCommand);
                        break;
                    }

                case OwlType.Video:
                    {
                        viewModel = new OwlVideoViewModel(model, authorizedUserId, itemTappedCommand, likeTappedCommand, saveTappedCommand);
                        break;
                    }

                case OwlType.NoMedia:
                    {
                        viewModel = new OwlNoMediaViewModel(model, authorizedUserId, itemTappedCommand, likeTappedCommand, saveTappedCommand);
                        break;
                    }

                default:
                    break;
            }

            return viewModel;
        }

        public static OwlModel ToModel(this OwlViewModel viewModel)
        {
            OwlModel model = null;

            if (viewModel != null)
            {
                model = new OwlModel
                {
                    Id = viewModel.Id,
                    Author = viewModel.Author,
                    Date = viewModel.Date,
                    LikesList = viewModel.LikesList,
                    SavesList = viewModel.SavesList,
                    Media = viewModel.Media,
                    MediaType = viewModel.MediaType,
                    Text = viewModel.Text,
                };
            }
            else
            {
                //viewmodel is null
            }

            return model;
        }
    }
}
