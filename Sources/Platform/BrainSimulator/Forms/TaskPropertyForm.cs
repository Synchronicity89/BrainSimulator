﻿using GoodAI.BrainSimulator.NodeView;
using GoodAI.Core.Task;
using GoodAI.Core.Utils;
using System.ComponentModel;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GoodAI.BrainSimulator.Forms
{
    public partial class TaskPropertyForm : DockContent
    {
        private MainForm m_mainForm;

        public MyTask Target
        {
            get { return propertyGrid.SelectedObject as MyTask; }
            set { propertyGrid.SelectedObject = value; }
        }

        public TaskPropertyForm(MainForm mainForm)
        {
            m_mainForm = mainForm;
            InitializeComponent();                   
            propertyGrid.BrowsableAttributes = new AttributeCollection(new MyBrowsableAttribute());
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //TODO: rewrite this, no need to loop all, just topmost
            foreach (GraphLayoutForm graphView in m_mainForm.GraphViews.Values)
            {
                if (graphView.Desktop.FocusElement is MyNodeView)
                {
                    MyNodeView nodeView = graphView.Desktop.FocusElement as MyNodeView;
                    nodeView.UpdateView();
                }                
                graphView.Desktop.Invalidate();
            }
        }

        public void RefreshGrid()
        {
            propertyGrid.Refresh();
        }

        private void dashboardButton_CheckedChanged(object sender, System.EventArgs e)
        {
            if (propertyGrid.SelectedGridItem.PropertyDescriptor == null)
                return;

            m_mainForm.DashboardPropertyToggle(Target, propertyGrid.SelectedGridItem.PropertyDescriptor.Name,
                dashboardButton.Checked);
        }

        private void propertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            RefreshDashboardButton();
        }

        private void propertyGrid_Enter(object sender, System.EventArgs e)
        {
            RefreshDashboardButton();
        }

        private void RefreshDashboardButton()
        {
            if (ActiveControl == propertyGrid && propertyGrid.SelectedGridItem != null && Target is MyTask)
            {
                PropertyDescriptor descriptor = propertyGrid.SelectedGridItem.PropertyDescriptor;
                if (descriptor == null)
                    return;

                // A real property has been selected.
                dashboardButton.Enabled = true;
                dashboardButton.Checked = m_mainForm.CheckDashboardContains(Target, descriptor.Name);
            }
            else
            {
                dashboardButton.Enabled = false;
            }
        }
    }
}
