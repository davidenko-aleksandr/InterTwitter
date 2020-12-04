<<<<<<< HEAD

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
>>>>>>> f3bc9c260b87115cba81921ffdd9f5c2b2600fbc
