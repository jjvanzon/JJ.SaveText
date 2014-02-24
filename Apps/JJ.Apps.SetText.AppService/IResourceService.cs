﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace JJ.Apps.SetText.AppService
{
    [ServiceContract]
    public interface IResourceService
    {
        [OperationContract]
        Messages GetMessages(string cultureName);

        [OperationContract]
        Labels GetLabels(string cultureName);

        [OperationContract]
        Titles GetTitles(string cultureName);
    }
}
