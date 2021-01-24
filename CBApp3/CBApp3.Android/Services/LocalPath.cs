using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using Xamarin.Forms;
using Xamarin.Essentials;

namespace CBApp3.Droid.Services
{
    public class LocalPath : CBApp3.Services.ILocalPath
    {
        public string GetFullPath(string fileName)
        {
            return Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)
                , fileName);
        }
    }
}