<<<<<<< HEAD:src/InterTwitter/ViewModels/HomePageItems/OwlVideoViewModel.cs
﻿using System.Linq;
using InterTwitter.Models;

namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlVideoViewModel : OwlViewModel
    {
        public OwlVideoViewModel(OwlModel model, UserModel author) : base(model, author)
        {
            Video = model.Media.First();
        }

        #region -- Public properties --

        private string _video;
        public string Video
        {
            get => _video;
            set => SetProperty(ref _video, value);
        }

        #endregion    
    }
=======
﻿using System.Linq;
using InterTwitter.Models;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlVideoViewModel : OwlViewModel
    {
        public OwlVideoViewModel(
            OwlModel model,
            UserModel author,
            int authorizedUserId)
            : base(model, author, authorizedUserId)
        {
            Video = model.Media.First();
        }

        #region -- Public properties --

        private string _video;
        public string Video
        {
            get => _video;
            set => SetProperty(ref _video, value);
        }

        #endregion    
    }
>>>>>>> f699f7fac03e1f21e36e38727bae9f097ad0be69:src/InterTwitter/ViewModels/OwlItems/OwlVideoViewModel.cs
}