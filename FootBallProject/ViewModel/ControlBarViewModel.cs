using FootBallProject.Class;
using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FootBallProject.ViewModel
{
    public class ControlBarViewModel:BaseViewModel
    {
      
        #region comands
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }


        #endregion
       

        public ControlBarViewModel()
        {
           
            CloseWindowCommand = new RelayCommand<UserControl>((p) => { return p != null ? true : false; }, 
                (p) => { 
                    FrameworkElement window = GetWindowParent(p);
                    var w = window as Window;
                    if (w !=null)
                    {
                        w.Close();
                    }

                });
            MinimizeWindowCommand = new RelayCommand<UserControl>((p) => { return p != null ? true : false; },
                (p) => {
                    FrameworkElement window = GetWindowParent(p);
                    var w = window as Window;
                    if (w != null)
                    {
                        w.WindowState= WindowState.Minimized;
                    }

                });
            MouseMoveWindowCommand = new RelayCommand<UserControl>((p) => { return p != null ? true : false; },
                (p) => {
                    FrameworkElement window = GetWindowParent(p);
                    var w = window as Window;
                    if (w != null)
                    {
                        w.DragMove();
                    }

                });
        }
        FrameworkElement GetWindowParent(UserControl p) {
            FrameworkElement parent = p;
            while (parent.Parent!= null) {

                parent = parent.Parent as FrameworkElement;

            }
            return parent;
        }
        
    }
}
