using DatabaseCleaner.Projects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatabaseCleaner
{
    internal partial class ProjectPreviewPopUp : Form
    {
        internal ProjectPreviewPopUp()
        {
            InitializeComponent();
        }

        internal ProjectPreviewPopUp(ProjectData project)
        {
            InitializeComponent();
            TB_ProjectTitle.Text = "Currently Viewing: " + project.ToString();
            RTB_CleanedPreview.Text = CreatePreview(project);
        }

        private void BT_ClosePreview_Click(object sender, EventArgs e)
        {
            this.Owner.Focus();
            this.Close();
        }

        private string CreatePreview(ProjectData project)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(project.Title);
            str.AppendLine("ID: " + project.Id);
            str.AppendLine("Bedrijf: " + project.Customer);
            str.AppendLine("Bedrijf Nieuw: " + project.ModernCustomer);
            str.AppendLine();
            if (project.StartDate.Equals(DateTime.MaxValue))
            { str.AppendLine("Start datum: "); }
            else
            { str.AppendLine("Start datum: " + project.StartDate.ToString("d")); }
            if (project.EndDate.Equals(DateTime.MinValue))
            { str.AppendLine("Eind datum: "); }
            else
            { str.AppendLine("Eind datum: " + project.EndDate.ToString("d")); }
            str.AppendLine("Uren: " + project.Hours);
            str.AppendLine("Project type: " + project.ProjectType.Trim());
            str.AppendLine("Ontwerp: " + project.Design);
            AppendTwice("Afgewezen?:", project.Declined);
            AppendTwice("Omschrijving:", project.Description);
            AppendTwice("Fasering Werkzaamheden:", project.Phase);
            AppendTwice("Opmerkingen:", project.Comment);
            AppendTwice("Toelichting:", project.Explanation);
            AppendTwice("Functionaliteit:", project.Functionality);
            AppendTwice("Toepassing:", project.Application);
            AppendTwice("Kennisinstelling:", project.Knowledge);
            AppendTwice("Doelgroep:", project.Audience);
            AppendTwice("Methode:", project.Method);
            AppendTwice("- Technische knelpunten:", project.TechProblem);
            AppendTwice("- Technische oplossingsrichtingen:", project.TechSolution);
            AppendTwice("- Technische nieuwheid:", project.TechNew);
            AppendTwice("Technologiegebied onderzoek:", project.TechResearch);
            AppendTwice("Vragen senter:", project.QuestionSenter);
            AppendTwice("Zelf:", project.Self);
            AppendTwice("Prin:", project.Prin);
            AppendTwice("Wordt er mede programmatuur ontwikkeld?:", project.SoftwareMade);
            return str.ToString();

            void AppendTwice(string prefix, object value)
            {
                str.AppendLine(prefix);
                str.AppendLine(value.ToString());
                str.AppendLine();
            }
        }
    }
}