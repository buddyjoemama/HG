using HumanGavel.Common.Storage;
using HumanGavel.Data;
using HumanGavel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaseGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Random catRandomizer = new Random(5);

            // Clean up
            using (HGDataContext context = new HGDataContext())
            {
                context.Database.ExecuteSqlCommand("delete from Votes");
                context.Database.ExecuteSqlCommand("delete from users");
                context.Database.ExecuteSqlCommand("delete from Participants");
                context.Database.ExecuteSqlCommand("delete from cases");
            }

            Console.WriteLine("Generating cases");

            MD5 hasher = MD5.Create();

            using (HGDataContext context = new HGDataContext())
            {
                // Create a bunch of cases
                for (int i = 0; i < 3000; i++)
                {
                    Case @case = new Case();
                    @case.Category = (Category)catRandomizer.Next(0, 5);
                    @case.CreateDateTimeUTC = DateTime.UtcNow;
                    @case.ExpirationDate = DateTime.UtcNow.AddDays(catRandomizer.Next(0, 365));
                    @case.Name = String.Format("{0} vs. {0} prime", i);
                    @case.Participants = new List<Participant>();
                    @case.IsEnabled = true;

                    @case.Participants.Add(new Participant
                    {
                        Name = "P: " + i,
                        NameHash = Guid.NewGuid().ToString()
                    });

                    @case.Participants.Add(new Participant
                    {
                        Name = "P: " + i + "`",
                        NameHash = Guid.NewGuid().ToString()
                    });

                    // Create an image.
                    System.Drawing.Bitmap b = new System.Drawing.Bitmap(1024, 500);
                    using (var graphics = Graphics.FromImage(b))
                    {
                        graphics.DrawRectangle(Pens.Gray, new Rectangle(0, 0, 1024, 500));

                        graphics.DrawString("Participant: " + i + " vs. " + i + " prime.",
                            new Font("Times New Roman", 32.0f, FontStyle.Regular),
                            new SolidBrush(Color.Black), 0, 20);
                    }

                    // Store the main image
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        b.Save(imgStream, ImageFormat.Png);
                        byte[] data = imgStream.ToArray();

                        Guid imgKey = new Guid(hasher.ComputeHash(data));
                        @case.ImageUrl = BlobStorageManager.Images.StoreBytes(data, imgKey).Uri.ToString();
                    }

                    // Store the thumb
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        b.GetThumbnailImage(368, 180, () => { return true; }, IntPtr.Zero)
                            .Save(imgStream, ImageFormat.Png);

                        byte[] data = imgStream.ToArray();
                        Guid imgKey = new Guid(hasher.ComputeHash(data));

                        @case.ThumbnailImageUrl = BlobStorageManager.Images.StoreBytes(data, imgKey).Uri.ToString();
                    }

                    context.Cases.Add(@case);
                    context.SaveChanges();
                }
            }

            Console.WriteLine("Generating users");

            // Create users
            using (HGDataContext context = new HGDataContext())
            {
                // Create some users
                for (int i = 0; i < 500; i++)
                {
                    User user = new User();
                    user.CreateDateTimeUTC = DateTime.UtcNow;

                    context.Users.Add(user);
                }

                context.SaveChanges();
            }

            Console.WriteLine("Voting");

            List<Case> allCases = Case.GetAllCases();

            // Users vote
            using (HGDataContext context = new HGDataContext())
            {
                var allParticipants = context.Participants.ToList();

                foreach (var u in context.Users.ToArray())
                {
                    Dictionary<Guid, int> votes = new Dictionary<Guid, int>();
        
                    // How many times will this user vote.            
                    for(int i = 0; i < catRandomizer.Next(100, 250); i++)
                    {
                        var caseIndex = catRandomizer.Next(0, allCases.Count);

                        // and for which case.
                        var caseToVoteOn = allCases[caseIndex];

                        // partipant to vote for
                        var p = caseToVoteOn.Participants.ToArray()[catRandomizer.Next(0, 2)];

                        try
                        {
                            User.CastVote(u.UserId, caseToVoteOn.CaseId, p.ParticipantId);
                        }
                        catch(ParticipantNotFoundException e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }
            }
        }
    }
}
