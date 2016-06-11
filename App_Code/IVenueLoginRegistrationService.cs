﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVenueLoginRegistrationService" in both code and config file together.
[ServiceContract]
public interface IVenueLoginRegistrationService
{
    [OperationContract]
    bool RegisterVenue(Venue v, VenueLogin vl);

    [OperationContract]
    int VenueLogin(String userName, string password);

    [OperationContract]
    bool AddShow(Show s, ShowDetail sd);

    [OperationContract]
    bool AddArtist(Artist a);

  

 
}

