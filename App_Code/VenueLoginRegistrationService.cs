using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VenueLoginRegistrationService" in code, svc and config file together.
public class VenueLoginRegistrationService : IVenueLoginRegistrationService
{
    VenueShowTrackerEntities db = new VenueShowTrackerEntities();

   
    public bool AddArtist(Artist a)
    {
        bool result = true;
        Artist artist = new Artist();
        artist.ArtistName = a.ArtistName;
        artist.ArtistEmail = a.ArtistEmail;
        artist.ArtistWebPage = a.ArtistWebPage;
        a.ArtistDateEntered = DateTime.Now;

        //add artist to database
        try
        {
            db.Artists.Add(artist);
            db.SaveChanges();

            //save changes
        }
        catch
        {
            result = false;
        }
        return result;
    }

   
    public int VenueLogin(string userName, string password)
    {
        int result = db.usp_venueLogin(userName, password);
        if (result != -1)
        {
            var key = from v in db.Venues
                      where v.VenueName.Equals(userName)
                      select new { v.VenueKey };
            foreach (var v in key)
            {
                result = (int)v.VenueKey;
            }
        }
        return result;
    }

    public bool RegisterVenue(Venue v, VenueLogin vl)
    {
        bool result = true;
        int pass = db.usp_RegisterVenue(
           v.VenueName,
           v.VenueAddress,
           v.VenueCity,
           v.VenueState,
           v.VenueZipCode,
           v.VenuePhone,
           v.VenueEmail,
           v.VenueWebPage,
           v.VenueAgeRestriction,
           vl.VenueLoginUserName,
           vl.VenueLoginPasswordPlain
          );

        if (pass == -1)
        {
            result = false;
        }
        return result;
    }

     public bool AddShow(Show sh, ShowDetailInfo sd )
    {
        
        Show s = new Show();
        s.ShowName = sh.ShowName;
        s.ShowDate = sh.ShowDate;
        s.ShowDateEntered = sh.ShowDateEntered;
        s.ShowTime = sh.ShowTime;
        s.ShowTicketInfo = sh.ShowTicketInfo;
        s.ShowKey = sh.ShowKey;


        ShowDetail showDet = new ShowDetail();
        showDet.ShowKey = sd.ShowKey;
        showDet.ArtistKey = sd.ArtistKey;
        showDet.ShowDetailArtistStartTime = sd.ArtistKeyShowDetailArtistStartTime.TimeOfDay;
        showDet.ShowDetailAdditional = sd.ShowDetailAdditional;
        bool result = true;
        try
        {
            db.Shows.Add(s);
            db.ShowDetails.Add(showDet);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            result = false;
          
        }
        return result;
    }


}
