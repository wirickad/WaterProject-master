﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterProject.Models
{
    public class EFDonationRepository : IDonationRepository
    {
        private WaterProjectContext context;

        public EFDonationRepository (WaterProjectContext temp)
        {
            context = temp;
        }
        public IQueryable<Donation> Donations => context.Donations.Include(x => x.Lines).ThenInclude(x => x.Project);

        public void SaveDonation(Donation donation)
        {
            context.AttachRange(donation.Lines.Select(x => x.Project));

            if (donation.DonationId == 0)
            {
                context.Donations.Add(donation);
            }

            context.SaveChanges();
        }
    }
}
