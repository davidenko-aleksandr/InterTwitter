using System;
using System.Linq;
using Prism.Common;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.Extensions
{
    public static class INavigationServiceExtensions
    {
        public static INavigationResult FixedSelectTab(this INavigationService navigationService, Type targetPageType, INavigationParameters parameters = null)
        {
            var result = new NavigationResult();

            try
            {
                Page targetPage = null;
                TabbedPage tabbedPage = null;
                MasterDetailPage currentPage = null;

                var page = ((IPageAware)navigationService).Page;

                if(page is MasterDetailPage mPage)
                {
                    currentPage = mPage;

                    if (currentPage.Detail is NavigationPage navPage)
                    {
                        if (navPage.CurrentPage is TabbedPage tPage)
                        {
                            tabbedPage = tPage;
                        }
                        else
                        {
                            throw new ArgumentException("Detail is not a Tab Page!");
                        }
                    }
                    else if (currentPage.Detail is TabbedPage tPage)
                    {
                        tabbedPage = tPage;
                    }
                    else
                    {
                        throw new ArgumentException("Tab Page not found!");
                    }
                }
                else
                {
                    throw new ArgumentException("Call outside of MasterDetailPage");
                }

                targetPage = tabbedPage.Children.First(x => x.GetType() == targetPageType);

                tabbedPage.CurrentPage = targetPage;
                
                PageUtilities.OnNavigatedFrom(currentPage, parameters);
                PageUtilities.OnNavigatedTo(targetPage, parameters);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

    }
}
