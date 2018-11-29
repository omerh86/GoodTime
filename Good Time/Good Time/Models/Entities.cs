using System;
using System.Collections.Generic;
using System.Text;

namespace Good_Time.Models
{
    public class UserResponseRootobject
    {
        public Document[] documents { get; set; }
    }

    public class Document
    {
        public string name { get; set; }
        public Fields fields { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
    }

    public class Fields
    {
        public Name name { get; set; }
        public Token token { get; set; }
        public Number number { get; set; }
    }

    public class Name
    {
        public string stringValue { get; set; }
    }

    public class Token
    {
        public string stringValue { get; set; }
    }

    public class Number
    {
        public string stringValue { get; set; }
    }


    public class FcmRootobject
    {
        public string to { get; set; }
        public Notification notification { get; set; }

        public FcmRootobject(string fromName, string token)
        {
            this.notification = new Notification(fromName);
            this.to = token;

        }
    }

    public class Notification
    {
        public string body { get; set; }
        public string title { get; set; }

        public Notification(string fromName)
        {
            this.title = fromName + " just Pink you!";
            this.body = "";
        }
    }


    public class StructuredqueryRootobject
    {
        public Structuredquery structuredQuery { get; set; }
    }

    public class Structuredquery
    {
        public Where where { get; set; }
        public From[] from { get; set; }
    }

    public class Where
    {
        public Fieldfilter fieldFilter { get; set; }
    }

    public class Fieldfilter
    {
        public Field field { get; set; }
        public string op { get; set; }
        public Value value { get; set; }
    }

    public class Field
    {
        public string fieldPath { get; set; }
    }

    public class Value
    {
        public string stringValue { get; set; }
    }

    public class From
    {
        public string collectionId { get; set; }

    }

    public class PingFrom
    {
        public string stringValue { get; set; }

    }

    public class PingRootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public PingDocument document { get; set; }
        public DateTime readTime { get; set; }
    }

    public class PingDocument
    {
        public string name { get; set; }
        public pingFields fields { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
    }

    public class pingFields
    {
        public Endtime endTime { get; set; }
        public PingFrom from { get; set; }
        public To to { get; set; }
    }

    public class Endtime
    {
        public string stringValue { get; set; }
    }

    public class To
    {
        public string stringValue { get; set; }
    }

}
