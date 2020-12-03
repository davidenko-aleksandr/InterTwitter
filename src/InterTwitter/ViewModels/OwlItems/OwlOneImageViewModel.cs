<<<<<<< HEAD:src/InterTwitter/ViewModels/HomePageItems/OwlOneImageViewModel.cs
﻿using System.Linq;
using InterTwitter.Models;

namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlOneImageViewModel : OwlViewModel
    {
        public OwlOneImageViewModel(OwlModel model, UserModel author) : base(model, author)
        {
            PostPhoto = model.Media.First();
        }

        #region -- Public properties --

        private string _postPhoto;
        public string PostPhoto
        {
            get => _postPhoto;
            set => SetProperty(ref _postPhoto, value);
        }

        #endregion        
    }
}
=======
﻿using System.Linq;
using InterTwitter.Models;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlOneImageViewModel : OwlViewModel
    {
        public OwlOneImageViewModel(
            OwlModel model,
            UserModel author,
            int authorizedUserId)
            : base(model, author, authorizedUserId)
        {
            PostPhoto = model.Media.First();
        }

        #region -- Public properties --

        private string _postPhoto;
        public string PostPhoto
        {
            get => _postPhoto;
            set => SetProperty(ref _postPhoto, value);
        }

        #endregion        
    }
}
>>>>>>> f699f7fac03e1f21e36e38727bae9f097ad0be69:src/InterTwitter/ViewModels/OwlItems/OwlOneImageViewModel.cs
