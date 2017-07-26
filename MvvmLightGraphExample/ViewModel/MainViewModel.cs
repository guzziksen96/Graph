using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmLightGraphExample.Model;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MvvmLightGraphExample.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        
        private GraphVM _graph;

        public GraphVM Graph
        {
            get { return _graph; }
            set { _graph = value;
                RaisePropertyChanged();
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _graph = new GraphVM();
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    
                });
        }

        public override void Cleanup()
        {
            // Clean up if needed

            base.Cleanup();
        }
    }
}