using BerrasBio.Models;
using Microsoft.EntityFrameworkCore;

namespace BerrasBio.Data
{
    public static class SeedData
    {
       public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BerrasBioContext(
                serviceProvider.GetRequiredService<DbContextOptions<BerrasBioContext>>()))
            {
                if (context.Movies.Any())
                {
                    return;
                }

                var movies = new Movie[]
                {
                    new Movie{ID=1, Title ="Spider Man - No Way Home", Description= "Very cool cross over", MoviePrice=110},
                    new Movie{ID=2, Title ="Mortal Kombat", Description= "A lot of action", MoviePrice=120},
                    new Movie{ID=3, Title ="Zack Snyder's - Justice League", Description= "Awesome", MoviePrice=140},
                    new Movie{ID=4, Title ="Joker", Description= "Interesting psycologic drama", MoviePrice=160}
                };

                foreach (var movie in movies)
                {
                    context.Movies.Add(movie);
                }
                context.SaveChanges();

                var salon = new Salon() { TotalSeats = 50 };
                context.Salons.Add(salon);
                context.SaveChanges();

                var displays = new Display[]
                {
                    new Display{StartingTime=DateTime.Parse("16:00"), MovieID=1,SalonID=1,SeatsLeft=15},
                    new Display{StartingTime=DateTime.Parse("18:00"), MovieID=2,SalonID=1,SeatsLeft=5},
                    new Display{StartingTime=DateTime.Parse("20:00"), MovieID=3,SalonID=1,SeatsLeft=20},
                    new Display{StartingTime=DateTime.Parse("22:00"), MovieID=4,SalonID=1,SeatsLeft=22}
                };
                foreach (var display in displays)
                {
                    context.Display.Add(display);
                }
                context.SaveChanges();
            }
        }
    }
}
