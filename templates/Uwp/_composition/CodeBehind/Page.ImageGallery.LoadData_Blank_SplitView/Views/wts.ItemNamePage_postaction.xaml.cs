﻿namespace Param_ItemNamespace.Views
{
    public sealed partial class wts.ItemNamePage : Page, INotifyPropertyChanged
    {
        public wts.ItemNamePage()
        {
        }

        //{[{
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                var selectedImageId = await ApplicationData.Current.LocalSettings.ReadAsync<string>(wts.ItemNameSelectedIdKey);
                if (!string.IsNullOrEmpty(selectedImageId))
                {
                    var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation(wts.ItemNameAnimationClose);
                    if (animation != null)
                    {
                        var item = ImagesGridView.Items.FirstOrDefault(i => ((SampleImage)i).ID == selectedImageId);
                        ImagesGridView.ScrollIntoView(item);
                        await ImagesGridView.TryStartConnectedAnimationAsync(animation, item, "galleryImage");
                    }

                    ApplicationData.Current.LocalSettings.SaveString(wts.ItemNameSelectedIdKey, string.Empty);
                }
            }
        }
        //}]}
    }
}