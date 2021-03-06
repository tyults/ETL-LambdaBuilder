﻿namespace org.ohdsi.cdm.framework.common2.Omop
{
   public class CareSite : Entity
   {
      public long LocationId { get; set; }
      public int OrganizationId { get; set; }
      public string PlaceOfSvcSourceValue { get; set; }

      // CDM v5 props
      public string Name { get; set; }

      public override string GetKey()
      {
         return Id + ":" + LocationId;
      }
   }
}
