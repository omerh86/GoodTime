using System;
using System.Collections.Generic;
using System.Text;


namespace Good_Time.Models
{
    public interface IPortableInterface
    {
        void test();
    }

    public interface IContactListener
    {
        void onUserChanged(List<User> contacts, bool isServerUser);
    }

    public interface IUsersResponseListener
    {
        void onUserResponse(UserResponseRootobject collection);
    }

}
