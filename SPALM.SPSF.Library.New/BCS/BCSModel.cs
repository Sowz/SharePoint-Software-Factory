namespace SPALM.SPSF.Library
{
    using System.Collections.Generic;

    public class BCSModel
    {
        public List<BCSEntity> Entities;

        public string Name;

        public BCSModel()
        {
            Entities = new List<BCSEntity>();
        }

        public bool IsValidAssociation(BCSField sourceField)
        {
            if (sourceField.ReferencedEntity == null)
            {
                return false;
            }

            if (sourceField.IsKey)
            {
                //keys cannot be part of an foreignkey
                //return false;
            }

            //check if the referenced entity and the referenced field is in the model
            foreach (BCSEntity entity in this.Entities)
            {
                if (entity.Name == sourceField.ReferencedEntity)
                {
                    foreach (BCSField field in entity.Fields)
                    {
                        if (field.Name == sourceField.ReferencedField)
                        {
                            //found
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
