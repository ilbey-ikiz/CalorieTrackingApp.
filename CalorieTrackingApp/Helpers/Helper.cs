using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WndPL.Forms;

namespace WndPL
{
    public class Helper
    {
        /// <summary>
        /// Displays a form inside a Guna2Panel by clearing its controls and adding the specified form as a child control.
        /// </summary>
        /// <param name="form">The form to be displayed.</param>
        /// <param name="pnl">The Guna2Panel control.</param>
        public void ShowPanel(Form form, Guna2Panel pnl)
        {
            pnl.Controls.Clear();
            form.TopLevel = false;
            pnl.Controls.Add(form);
            form.Show();
        }

        /// <summary>
        /// Clears the text of all Guna2TextBox controls inside a form, except for the specified Guna2TextBox control.
        /// </summary>
        /// <param name="form">The form containing the Guna2TextBox controls.</param>
        /// <param name="textBox">The Guna2TextBox control to exclude from clearing.</param>
        public void ClearTextBoxes(Control.ControlCollection controls)
        {
            foreach (var item in controls)
            {
                if (item is Guna2TextBox txt)
                    txt.Text = null;
            }
        }

        /// <summary>
        /// Checks if all Guna2TextBox controls inside a form are empty.
        /// </summary>
        /// <param name="form">The form containing the Guna2TextBox controls.</param>
        /// <returns>True if all Guna2TextBox controls are empty, false otherwise.</returns>
        public bool AreTextBoxesEmpty(Form form)
        {
            bool IsEmpty = false;
            foreach (var item in form.Controls)
            {

                if (item is Guna2TextBox txt)
                    if (string.IsNullOrWhiteSpace(txt.Text))

                        IsEmpty = true;
            }
            return IsEmpty;
        }

        /// <summary>
        /// Checks if all Guna2TextBox controls inside a Guna2Panel are empty.
        /// </summary>
        /// <param name="panel">The Guna2Panel containing the Guna2TextBox controls.</param>
        /// <returns>True if all Guna2TextBox controls are empty, false otherwise.</returns>
        public bool AreTextBoxesEmpty(Guna2Panel panel)
        {
            bool IsEmpty = false;
            foreach (var item in panel.Controls)
            {

                if (item is Guna2TextBox txt)
                    if (string.IsNullOrWhiteSpace(txt.Text))

                        IsEmpty = true;
            }
            return IsEmpty;
        }

        /// <summary>
        /// Checks if all Guna2TextBox controls inside a Guna2Panel, except for the specified Guna2TextBox control, are empty.
        /// </summary>
        /// <param name="panel">The Guna2Panel containing the Guna2TextBox controls.</param>
        /// <param name="textBox">The Guna2TextBox control to exclude from the check.</param>
        /// <returns>True if all other Guna2TextBox controls are empty, false otherwise.</returns>
        public bool AreTextBoxesEmptyExceptOne(Guna2Panel panel, Guna2TextBox textBox)
        {
            bool IsEmpty = false;
            foreach (var item in panel.Controls)
            {
                if (item is Guna2TextBox txt && textBox != item)
                    if (string.IsNullOrWhiteSpace(txt.Text))
                        IsEmpty = true;
            }
            return IsEmpty;
        }

        /// <summary>
        /// Hides the first form, shows the second form as a modal dialog, and then shows the first form again.
        /// </summary>
        /// <param name="from1">The first form to hide.</param>
        /// <param name="form2">The second form to show as a modal dialog.</param>
        public void HideAndShow(Form from1, Form form2)
        {
            from1.Hide();
            form2.ShowDialog();
            from1.Show();
        }

        /// <summary>
        /// Checks if all text in the Guna2TextBox controls inside a Guna2Panel start and end with a digit.
        /// </summary>
        /// <param name="panel">The Guna2Panel containing the Guna2TextBox controls.</param>
        /// <returns>True if all text in the Guna2TextBox controls start and end with a digit, false otherwise.</returns>
        public bool StartAndEndWithDigit(Guna2Panel panel)
        {
            bool result = true;
            foreach (var item in panel.Controls)
            {
                if (item is Guna2TextBox txt)
                        if (!(char.IsDigit(txt.Text[0]) && char.IsDigit(txt.Text[^1])))
                            result = false;
            }
            return result;
        }

        /// <summary>
        /// Checks if all text in the Guna2TextBox controls inside a Guna2Panel, except for the specified Guna2TextBox control, start and end with a digit.
        /// </summary>
        /// <param name="panel">The Guna2Panel containing the Guna2TextBox controls.</param>
        /// <param name="textBox">The Guna2TextBox control to exclude from the check.</param>
        /// <returns>True if all other Guna2TextBox controls' text start and end with a digit, false otherwise.</returns>
        public bool StartAndEndWithDigitExceptOne(Guna2Panel panel,Guna2TextBox textBox)
        {
            bool result = true;
            foreach (var item in panel.Controls)
            {
                if (item is Guna2TextBox txt && textBox!=item)
                    if (!(char.IsDigit(txt.Text[0]) && char.IsDigit(txt.Text[^1])))
                        result = false;
            }
            return result;
        }
    }
}
