using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using System.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace TravelingApp
{
    [Activity(Label = "SearchActivity", Theme = "@style/MyTheme")]
    public class SearchActivity : AppCompatActivity
    {
        private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private NavigationView mNavView;
        private AdapterView mHomeItem;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SearchPage);

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar_search);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout_search);

            SetSupportActionBar(mToolbar);

            

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            SlidingTabsFragment fragment = new SlidingTabsFragment();
            transaction.Replace(Resource.Id.search_fragment, fragment);
            transaction.Commit();
            try
            {

                mDrawerToggle = new MyActionBarDrawerToggle(
                    this,                           //Host Activity
                    mDrawerLayout,                  //DrawerLayout
                    Resource.String.openDrawer,     //Opened Message
                    Resource.String.closeDrawer     //Closed Message
                    );
                mDrawerLayout.SetDrawerListener(mDrawerToggle);  //gives drawer toggle permission to call the methods
                SupportActionBar.SetHomeButtonEnabled(true);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetDisplayShowTitleEnabled(true);
                mDrawerToggle.SyncState();
            }
            catch
            {

            }

            mNavView = FindViewById<NavigationView>(Resource.Id.nav_view_search);
            mHomeItem = FindViewById<AdapterView>(Resource.Id.nav_home);
            mNavView.NavigationItemSelected += MNavView_NavigationItemSelected;

            if (bundle != null)
            {
                if (bundle.GetString("DrawerState") == "Opened")
                {
                    SupportActionBar.SetTitle(Resource.String.openDrawer);
                }
                else
                {
                    SupportActionBar.SetTitle(Resource.String.closeDrawer);
                }
            }
            else
            {
                //first time activity is ran
                SupportActionBar.SetTitle(Resource.String.closeDrawer);
            }

        }

        private void MNavView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    Toast.MakeText(this, "Profile!", ToastLength.Short).Show();
                    Intent intent = new Intent(this, typeof(ProfileActivity));
                    StartActivity(intent);
                    break;
                case (Resource.Id.nav_search):
                    Toast.MakeText(this, "Search!", ToastLength.Short).Show();
                    Intent intentSearch = new Intent(this, typeof(SearchActivity));
                    StartActivity(intentSearch);
                    break;
                case (Resource.Id.nav_convert):
                    Toast.MakeText(this, "Convert!", ToastLength.Short).Show();
                    Intent intentConvert = new Intent(this, typeof(MoneyConvertActivity));
                    StartActivity(intentConvert);
                    break;
                case (Resource.Id.nav_logout):
                    Intent intentLogout = new Intent(this, typeof(MainActivity));
                    StartActivity(intentLogout);
                    break;
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
            }
            else
            {
                outState.PutString("DrawerState", "Closed");
            }
            base.OnSaveInstanceState(outState);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            mDrawerToggle.SyncState();
            base.OnPostCreate(savedInstanceState);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawerToggle.OnOptionsItemSelected(item);  //allows to be opened or closed
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}