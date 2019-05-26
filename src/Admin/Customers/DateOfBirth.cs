using System;

namespace Admin.Customers
{
    public class DateOfBirth
    {
        private readonly DateTime dateOfBirth;

        public DateOfBirth(DateTime dateOfBirth)
        {
            this.dateOfBirth = dateOfBirth;
        }

        public YearOfBirth Year => new YearOfBirth(this.dateOfBirth.Year);

        public int Month => this.dateOfBirth.Month;

        public int Day => this.dateOfBirth.Day;


        public override string ToString()
        {
            return this.dateOfBirth.ToString("yyyy-MM-dd");
        }


        public class YearOfBirth
        {
            private readonly int yearOfBirth;

            public YearOfBirth(int yearOfBirth)
            {
                if (TooOld(yearOfBirth) || TooYoung(yearOfBirth))
                {
                    throw new ArgumentOutOfRangeException(nameof(yearOfBirth));
                }

                this.yearOfBirth = yearOfBirth;
            }

            public override string ToString()
            {
                return this.yearOfBirth.ToString();
            }

            private static bool TooOld(int yearOfBirth)
            {
                return yearOfBirth < DateTime.Now.Year - 200;
            }

            private static bool TooYoung(int yearOfBirth)
            {
                return yearOfBirth >= DateTime.Now.Year - 18;
            }

            public static implicit operator int(YearOfBirth yearOfBirth)
            {
                return yearOfBirth.yearOfBirth;
            }

            public static explicit operator YearOfBirth(int yearOfBirth)
            {
                return new YearOfBirth(yearOfBirth);
            }
        }
    }
}
