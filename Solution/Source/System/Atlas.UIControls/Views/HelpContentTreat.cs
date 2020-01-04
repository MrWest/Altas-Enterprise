using System;
using System.Collections.Generic;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace CompanyName.Atlas.UIControls.Views
{
    public class HelpContentTreat: BindableBase
    {
        private IAtlasModuleGenericSubjectPresenter _moduleGenericSubject;
        private ISubjectConceptPresenter _presenter;

        public HelpContentTreat()
        {
            NavigationHstory = new List<ISubjectConceptPresenter>();
                
              MoveNextCommand= new DelegateCommand(ExecuteMethod, CanExecuteMethod);
              MovePreviousCommand = new DelegateCommand(Prvious,CanPrevius);
        }

        private bool CanPrevius()
        {
            return historyIndex >0  && !Equals(NavigationHstory) && NavigationHstory.Count >= 1 ;
        }

        private void Prvious()
        {
            if(CanPrevius())
            historyIndex--;
            OnPropertyChanged(() => SubjectConceptPresenter);
            MovePreviousCommand = new DelegateCommand(Prvious, CanPrevius);
            MoveNextCommand = new DelegateCommand(ExecuteMethod, CanExecuteMethod);
        }

        private bool CanExecuteMethod()
        {
            return !Equals(NavigationHstory) && historyIndex < NavigationHstory.Count - 1&& NavigationHstory.Count>0;
        }

        private void ExecuteMethod()
        {
            if(CanExecuteMethod())
            historyIndex++;
            OnPropertyChanged(()=>SubjectConceptPresenter);
            MoveNextCommand = new DelegateCommand(ExecuteMethod, CanExecuteMethod);
            MovePreviousCommand = new DelegateCommand(Prvious, CanPrevius);
        }

        public IAtlasModuleGenericSubjectPresenter ModuleGenericSubject
        {
            get { return _moduleGenericSubject; }
            set
            {
                _moduleGenericSubject = value;
                OnPropertyChanged(()=>ModuleGenericSubject);
                MoveNextCommand = new DelegateCommand(ExecuteMethod, CanExecuteMethod);
                MovePreviousCommand = new DelegateCommand(Prvious, CanPrevius);
                OnPropertyChanged(() => SubjectConceptPresenter);
            }
            
        }

        public ISubjectConceptPresenter SubjectConceptPresenter
        {
            get
            {
                if(NavigationHstory.Count>0&& historyIndex>=0)
                return NavigationHstory[historyIndex];

                return _presenter;
            }
            set
            {
                if(!Equals(value,null))
                {
                    NavigationHstory.Add(value);
                    historyIndex ++;
                }
               

                _presenter = value;
                
                OnPropertyChanged(()=>SubjectConceptPresenter);
                MoveNextCommand = new DelegateCommand(ExecuteMethod, CanExecuteMethod);
                MovePreviousCommand = new DelegateCommand(Prvious, CanPrevius);
                OnPropertyChanged(() => ModuleGenericSubject);
            }
        }

        private int historyIndex = -1;
        private ICommand _moveNextCommand;
        private ICommand _movePreviousCommand;

        private IList<ISubjectConceptPresenter> NavigationHstory { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed by the Next Button of the current <see cref="Paginator"/>.
        /// </summary>
        public ICommand MoveNextCommand
        {
            get { return _moveNextCommand; }
            set
            {
                _moveNextCommand = value;
                OnPropertyChanged(()=>MoveNextCommand);
            } }

        /// <summary>
        /// Gets or sets the command that is executed by the Next Button of the current <see cref="Paginator"/>.
        /// </summary>
        public ICommand MovePreviousCommand
        {
            get
            {
                return _movePreviousCommand;
                
            }
            set
            {
                _movePreviousCommand = value;
                OnPropertyChanged(()=>MovePreviousCommand);
            }
        }
    }
}