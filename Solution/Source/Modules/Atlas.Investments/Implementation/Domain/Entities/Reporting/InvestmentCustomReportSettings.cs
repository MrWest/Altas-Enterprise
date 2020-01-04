using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities.Reporting
{
    public class InvestmentCustomReportSettings: CustomReportSettings, IInvestmentCustomReportSettings
    {
        private bool _showResources;
        private bool _showEquipment;
        private bool _showConstruction;
        private bool _showOthers;
        private bool _showWorkCapital;

     

        private bool _showBudgetComponents;

        private bool _showSubExpenseConcept;
        private bool _showCategory;

        private bool _showSubSpecialities;

        public bool ShowSubSpecialities
        {

            get { return _showSubSpecialities; }
            set
            {
                _showSubSpecialities = value;
                if (_showSubSpecialities)
                {
                    ShowSubExpeseConcepts = false;
                    ShowCategories = false;
                }


            }
        }

        public bool ShowSubExpeseConcepts
        {
            get { return _showSubExpenseConcept; }
            set
            {
                _showSubExpenseConcept = value;
                if (_showSubExpenseConcept)
                {
                    ShowCategories = false;
                    ShowSubSpecialities = false;
                }


            }

        }

        public bool ShowCategories
        {
            get { return _showCategory; }
            set
            {
                _showCategory = value;
                if (_showCategory)
                {
                    ShowSubExpeseConcepts = false;
                    ShowSubSpecialities = false;
                }


            }
        }

        private bool Any()
        {
            return _showEquipment || _showConstruction || _showOthers || _showWorkCapital;
        }
        public bool ShowInvestmentElements { get; set; }

        public bool ShowBudgetComponents
        {
            get { return _showBudgetComponents; }
            set
            {
                _showBudgetComponents = value;
                if (_showBudgetComponents && !Any())
                {
                    _showEquipment = true;
                    _showConstruction = true;
                    _showOthers = true;
                    _showWorkCapital = true;
                }

                if (!_showBudgetComponents)
                {
                    _showEquipment = false;
                    _showConstruction = false;
                    _showOthers = false;
                    _showWorkCapital = false;
                }

               
            }
        }

       

        public bool ShowActivities { get; set; }
        public bool ShowResources { get; set; }

        public bool ShowEquipment
        {
            get { return _showEquipment; }
            set
            {
                _showEquipment = value;
                if ((_showEquipment && !ShowBudgetComponents) || (!_showEquipment && !Any()))
                    ShowBudgetComponents = _showEquipment;

            }
        }

        public bool ShowConstruction
        {
            get { return _showConstruction; }
            set
            {
                _showConstruction = value;
                if ((_showConstruction && !ShowBudgetComponents) || (!_showConstruction && !Any()))
                    ShowBudgetComponents = _showConstruction;
            }
        }

        public bool ShowOthers
        {
            get { return _showOthers; }
            set
            {
                _showOthers = value;
                if ((_showOthers && !ShowBudgetComponents) || (!_showOthers && !Any()))
                    ShowBudgetComponents = _showOthers;
            }
        }

        public bool ShowWorkCapital
        {

            get { return _showWorkCapital; }
            set
            {
                _showWorkCapital = value;
                if ((_showWorkCapital && !ShowBudgetComponents) || (!_showWorkCapital && !Any()))
                    ShowBudgetComponents = _showWorkCapital;
            }
        }
        public bool ShowMU { get; set; }
        public bool ShowQuantity { get; set; }
        public bool ShowCurrency { get; set; }
        public bool ShowUC { get; set; }
        public bool ShowCost { get; set; }

      
    }

    public class InvestmentMainCustomReportSettings : MainCustomReportSettings, IInvestmentMainCustomReportSettings
    {
      

        

        public InvestmentMainCustomReportSettings()
        {
                 ShowInvestmentElements  = true;
                 ShowBudgetComponents  = true;
                 ShowSubSpecialities  = true;
                 ShowActivities  = true;
                 ShowResources  = true;
                 ShowEquipment  = true;
                 ShowConstruction = true;
                 ShowOthers  = true;
                 ShowWorkCapital  = true;
                 ShowMU  = true;
                 ShowQuantity  = true;
                 ShowCurrency  = true;
                 ShowUC  = true;
                 ShowCost  = true;
      }
        private bool _showResources;
        private bool _showEquipment;
        private bool _showConstruction;
        private bool _showOthers;
        private bool _showWorkCapital;

        private bool _showBudgetComponents;

        private bool _showSubExpenseConcept;
        private bool _showCategory;

        private bool _showSubSpecialities;

        public bool ShowSubSpecialities
        {

            get { return _showSubSpecialities; }
            set
            {
                _showSubSpecialities = value;
                if (_showSubSpecialities)
                {
                    ShowSubExpeseConcepts = false;
                    ShowCategories = false;
                }


            }
        }

        public bool ShowSubExpeseConcepts
        {
            get { return _showSubExpenseConcept; }
            set
            {
                _showSubExpenseConcept = value;
                if (_showSubExpenseConcept)
                {
                    ShowCategories = false;
                    ShowSubSpecialities = false;
                }


            }

        }

        public bool ShowCategories
        {
            get { return _showCategory; }
            set
            {
                _showCategory = value;
                if (_showCategory)
                {
                    ShowSubExpeseConcepts = false;
                    ShowSubSpecialities = false;
                }


            }
        }

        private bool Any()
        {
            return _showEquipment || _showConstruction || _showOthers || _showWorkCapital;
        }
        public bool ShowInvestmentElements { get; set; }

        public bool ShowBudgetComponents
        {
            get { return _showBudgetComponents; }
            set
            {
                _showBudgetComponents = value;
                if (_showBudgetComponents && !Any())
                {
                    _showEquipment = true;
                    _showConstruction = true;
                    _showOthers = true;
                    _showWorkCapital = true;
                }

                if (!_showBudgetComponents)
                {
                    _showEquipment = false;
                    _showConstruction = false;
                    _showOthers = false;
                    _showWorkCapital = false;
                }

             
            }
        }
      //  public bool ShowSubSpecialities { get; set; }
        public bool ShowActivities { get; set; }
        public bool ShowResources { get; set; }

        public bool ShowEquipment
        {
            get { return _showEquipment; }
            set
            {
                _showEquipment = value;
                if ((_showEquipment && !ShowBudgetComponents)||(!_showEquipment && !Any()))
                    ShowBudgetComponents = _showEquipment;
               
            }
        }

        public bool ShowConstruction
        {
            get { return _showConstruction; }
            set
            {
                _showConstruction = value;
                if ((_showConstruction && !ShowBudgetComponents) || (!_showConstruction && !Any()))
                    ShowBudgetComponents = _showConstruction;
            }
        }

        public bool ShowOthers
        {
            get { return _showOthers; }
            set
            {
                _showOthers = value;
                if ((_showOthers && !ShowBudgetComponents) || (!_showOthers && !Any()))
                    ShowBudgetComponents = _showOthers;
            }
        }

        public bool ShowWorkCapital
        {

            get { return _showWorkCapital; }
            set
            {
                _showWorkCapital = value;
                if ((_showWorkCapital && !ShowBudgetComponents) || (!_showWorkCapital && !Any()))
                    ShowBudgetComponents = _showWorkCapital;
            }
        }
        public bool ShowMU { get; set; }
        public bool ShowQuantity { get; set; }
        public bool ShowCurrency { get; set; }
        public bool ShowUC { get; set; }
        public bool ShowCost { get; set; }
    }

    public class InvestmentChildCustomReportSettings : ChildCustomReportSettings, IInvestmentChildCustomReportSettings
    {
        public InvestmentChildCustomReportSettings()
        {
            ShowInvestmentElements = true;
            ShowBudgetComponents = true;
            ShowSubSpecialities = true;
            ShowActivities = true;
            ShowResources = true;
            ShowEquipment = true;
            ShowConstruction = true;
            ShowOthers = true;
            ShowWorkCapital = true;
            ShowMU = true;
            ShowQuantity = true;
            ShowCurrency = true;
            ShowUC = true;
            ShowCost = true;
        }
        private bool _showResources;
        private bool _showEquipment;
        private bool _showConstruction;
        private bool _showOthers;
        private bool _showWorkCapital;

        private bool _showSubExpenseConcept;
        private bool _showCategory;

        private bool _showSubSpecialities;

        public bool ShowSubSpecialities
        {

            get { return _showSubSpecialities; }
            set
            {
                _showSubSpecialities = value;
                if (_showSubSpecialities)
                {
                    ShowSubExpeseConcepts = false;
                    ShowCategories = false;
                }


            }
        }

        public bool ShowSubExpeseConcepts
        {
            get { return _showSubExpenseConcept; }
            set
            {
                _showSubExpenseConcept = value;
                if (_showSubExpenseConcept)
                {
                    ShowCategories = false;
                    ShowSubSpecialities = false;
                }


            }

        }

        public bool ShowCategories
        {
            get { return _showCategory; }
            set
            {
                _showCategory = value;
                if (_showCategory)
                {
                    ShowSubExpeseConcepts = false;
                    ShowSubSpecialities = false;
                }


            }
        }
        private bool _showBudgetComponents;

        private bool Any()
        {
            return _showEquipment || _showConstruction || _showOthers || _showWorkCapital;
        }
        public bool ShowInvestmentElements { get; set; }

        public bool ShowBudgetComponents
        {
            get { return _showBudgetComponents; }
            set
            {
                _showBudgetComponents = value;
                if (_showBudgetComponents && !Any())
                {
                    _showEquipment = true;
                    _showConstruction = true;
                    _showOthers = true;
                    _showWorkCapital = true;
                }

                if (!_showBudgetComponents)
                {
                    _showEquipment = false;
                    _showConstruction = false;
                    _showOthers = false;
                    _showWorkCapital = false;
                }

              
            }
        }
     ///   public bool ShowSubSpecialities { get; set; }
        public bool ShowActivities { get; set; }
        public bool ShowResources { get; set; }

        public bool ShowEquipment
        {
            get { return _showEquipment; }
            set
            {
                _showEquipment = value;
                if ((_showEquipment && !ShowBudgetComponents) || (!_showEquipment && !Any()))
                    ShowBudgetComponents = _showEquipment;

            }
        }

        public bool ShowConstruction
        {
            get { return _showConstruction; }
            set
            {
                _showConstruction = value;
                if ((_showConstruction && !ShowBudgetComponents) || (!_showConstruction && !Any()))
                    ShowBudgetComponents = _showConstruction;
            }
        }

        public bool ShowOthers
        {
            get { return _showOthers; }
            set
            {
                _showOthers = value;
                if ((_showOthers && !ShowBudgetComponents) || (!_showOthers && !Any()))
                    ShowBudgetComponents = _showOthers;
            }
        }

        public bool ShowWorkCapital
        {

            get { return _showWorkCapital; }
            set
            {
                _showWorkCapital = value;
                if ((_showWorkCapital && !ShowBudgetComponents) || (!_showWorkCapital && !Any()))
                    ShowBudgetComponents = _showWorkCapital;
            }
        }
        public bool ShowMU { get; set; }
        public bool ShowQuantity { get; set; }
        public bool ShowCurrency { get; set; }
        public bool ShowUC { get; set; }
        public bool ShowCost { get; set; }
    }
}