namespace Grader.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxPoints { get; set; }

        // Self calculated from here
        public int SumPoints = 0;
        public int NGrades = 0;
        public int Avg = 0;
        public int Max = 0;
        public int Grade = 0;
        public int Potential = 0;

        public List<int> Grades = new List<int>();

        public void calcGrades()
        {
            this.SumPoints = 0;
            this.NGrades = 0;
            this.Max = 0;

            foreach (var grade in this.Grades)
            {
                this.SumPoints += grade;
                this.NGrades += 1;
                this.Max = this.Max > grade ? this.Max : grade;
            }

            this.Avg = this.NGrades != 0 ? this.SumPoints / this.NGrades : 0;
            this.Grade = this.Avg / 2 + this.Max / 2;

            this.Potential = this.NGrades != 0 ? 
                (this.SumPoints + this.MaxPoints) / (this.NGrades + 1) / 2 + this.MaxPoints / 2 - this.Grade
                : this.MaxPoints;
        }

    }
}
