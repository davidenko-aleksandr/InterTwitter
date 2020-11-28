using InterTwitter.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlFewImagesViewModel : OwlViewModel
    {
        public OwlFewImagesViewModel(OwlModel model, UserModel author) : base(model, author)
        {
            //PostPhotoOne = model.Media[0];
            //PostPhotoTwo = model.Media[1];
            //PostPhotoThree = model.Media[2]; //TODO rework after new ListView
            Media = new ObservableCollection<string>()
                {
                    "https://icdn.lenta.ru/images/2020/01/28/17/20200128170822958/square_320_9146846fb3b1bfae5672755bc1896214.jpg",
                    "https://s0.rbk.ru/v6_top_pics/media/img/7/06/755581025099067.jpeg",
                    "https://static.toiimg.com/thumb/msid-67586673,width-800,height-600,resizemode-75,imgsize-3918697,pt-32,y_pad-40/67586673.jpg",
                    "https://www.humanesociety.org/sites/default/files/styles/1240x698/public/2020-07/kitten-510651.jpg?h=f54c7448&itok=ZhplzyJ9",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQo4BQSpilYy5KuAptMxbOAxm4uKjFYDG6_wg&usqp=CAU",
                    "https://img.huffingtonpost.com/asset/5e848c4825000056010586d9.jpeg?ops=1778_1000",

                };
        }

    #region -- Public properties --

    private string _postPhotoOne;
        public string PostPhotoOne
        {
            get => _postPhotoOne;
            set => SetProperty(ref _postPhotoOne, value);
        }

        private string _postPhotoTwo;
        public string PostPhotoTwo
        {
            get => _postPhotoTwo;
            set => SetProperty(ref _postPhotoTwo, value);
        }

        private string _postPhotoThree;
        public string PostPhotoThree
        {
            get => _postPhotoThree;
            set => SetProperty(ref _postPhotoThree, value);
        }

        private ObservableCollection<string> _media;
        public ObservableCollection<string> Media
        {
            get => _media;
            set => SetProperty(ref _media, value);
        }

        #endregion
    }
}
