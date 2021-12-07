using MintGarage.Models.AboutUsT.TeamMembers;
using MintGarage.Models.AboutUsT.Values;
using System.Collections.Generic;

namespace MintGarage.Models
{
    public class AboutUsModel
    {
        public IEnumerable<TeamMember> TeamMembers { get; set; }
        public TeamMember TeamMember { get; set; }
        public IEnumerable<Value> Values { get; set; }
        public Value Value { get; set; }

        public string ImageFile { get; } = "~/Images/aboutus/";
        public string TabImage { get; } = "construction.jpg";
        public string TabImageSlogon { get; } = "STORAGE WITH A DIFFERENCE";
        public string CEO { get; } = "CEO & FOUNDER";
        public string CEOName { get; } = "James Secondino";
        public string CEOImage { get; } = "james.jpg";
        public string CEODesc { get;  } = "For those of you who don’t know me well, I am an extremely organized person. Those of you who do know me well would say, “extremely” is an understatement! I would simply say that I am passionate about knowing where things are when  I need them and things just look better when they are in order, wouldn’t you agree?";
        public string FloorImage { get; } = "floorpic.jpg";
        public string AboutBusiness { get; } = "Transforming Inspiring Spaces";
        public string ValueTitle { get; } = "Our Core Values";
        public string ValueDesc { get; } = "At Mint Garage we believe in transforming spaces to the needs, desires and wants of our clients. Making sure no 2 garages are a like.";
        public string TeamTitle { get; } = "Meet Our Team";
        public string TeamDesc { get; } = "Our team consists of highly qualified, experienced and knowledgeable professionals who are dedicated to help our customers in any tranforming or storage projects!";
    }
}
