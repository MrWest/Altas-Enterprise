using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasMediaElement : MediaElement
    {
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ViewStateProperty = DependencyProperty.Register("ViewState", typeof(AtlasViewState), typeof(AtlasMediaElement), new PropertyMetadata(AtlasViewState.FrontPage,PropertyChangedCallback));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty IsSomeModuleLoadedProperty = DependencyProperty.Register("IsSomeModuleLoaded", typeof(bool), typeof(AtlasMediaElement), new PropertyMetadata(false, PropertyChangedCallback2));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
           
            //var mediaElement = (AtlasMediaElement) dependencyObject;
            //var state = (AtlasViewState) dependencyPropertyChangedEventArgs.NewValue;
            //if(state==AtlasViewState.Module)
            //    mediaElement.Stop();
            //else
            //{
            //    mediaElement.Play();
            //}
        }
        private static void PropertyChangedCallback2(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var mediaElement = (AtlasMediaElement)dependencyObject;
            var state = (bool)dependencyPropertyChangedEventArgs.NewValue;
            if (state)
                mediaElement.Stop();
            else
            {
                mediaElement.Play();
            }
        }
        public AtlasMediaElement()
        {
            DefaultStyleKey = typeof (AtlasMediaElement);
            Loaded+=OnLoaded;
            MediaEnded+=OnMediaEnded;
            IsHitTestVisibleChanged+=OnIsHitTestVisibleChanged;
            IsVisibleChanged+=OnIsVisibleChanged;
            LoadedBehavior = MediaState.Manual;
           
        }

        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public AtlasViewState ViewState
        {
            get { return (AtlasViewState)GetValue(ViewStateProperty); }
            set { SetValue(ViewStateProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public bool IsSomeModuleLoaded
        {
            get { return (bool)GetValue(IsSomeModuleLoadedProperty); }
            set { SetValue(IsSomeModuleLoadedProperty, value); }
        }
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
           
            if ((bool)dependencyPropertyChangedEventArgs.NewValue)
                Play();
            else
            {
                Stop();
            }
        }

        private void OnIsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            
            if((bool)dependencyPropertyChangedEventArgs.NewValue )
                Play();
            else
            {
                Stop();
            }

        }

        private void OnMediaEnded(object sender, RoutedEventArgs routedEventArgs)
        {
         
            //this.Play();
            Stop();
            Play();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //Play();
           // throw new NotImplementedException();
        }
    }
}
