namespace Grader.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double MaxPoints { get; set; }

        // Self calculated from here
        public double SumPoints = 0;
        public double NGrades = 0;
        public double Avg = 0;
        public double Max = 0;
        public double Grade = 0;
        public double Potential = 0;

        public List<double> Grades = new List<double>();

        public void init(double rootMaxPoints)
        {
            this.SumPoints = 0;
            this.NGrades = 0;
            this.Max = 0;

            foreach (double grade in this.Grades)
            {
                double gradeP = grade * this.MaxPoints / 100;
                this.SumPoints += gradeP;
                this.NGrades += 1;
                this.Max = this.Max > gradeP ? this.Max : gradeP;
            }

            this.Avg = this.NGrades != 0 ? this.SumPoints / this.NGrades : 0;
            this.Grade = this.Avg / 2 + this.Max / 2;

            this.Potential = this.NGrades != 0 ? 
                (this.SumPoints + this.MaxPoints) / (this.NGrades + 1) / 2 + this.MaxPoints / 2 - this.Grade
                : this.MaxPoints;
            this.Potential = this.Potential * 100 / rootMaxPoints;
        }
    }
}
