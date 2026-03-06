using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace CodeBase.Inventory
{
    public class Identifier
    {
        public string GetIdByName(string name, List<string> existingIds)
        {
            name = name is null ? string.Empty : new string(name).ToLower();
            
            string id = "";
            string separator = " ";

            if (name.IsEmpty())
            {
                separator = string.Empty;
            }
            else if (IsExistingId(name, existingIds) == false)
            {
                return name;
            }
            
            for (int i = 0; i < int.MaxValue; i++)
            {
                id = $"{name}{separator}{i}";
                
                if (IsExistingId(id, existingIds) == false)
                    break; 
            }
            
            return id;
        }

        public List<string> GetIdsByNames(List<string> names, List<string> existingIds)
        {
            List<string> result = new(names.Count);

            existingIds = existingIds is null ? new List<string>() : new List<string>(existingIds);
            
            foreach (var name in names)
            {
                string id = GetIdByName(name, existingIds);
                
                result.Add(id);
                existingIds.Add(id);
            }

            return result;
        }
        
        private bool IsExistingId(string id, List<string> ids)
        {
            if (ids is null)
                return false;
            
            foreach (var existingId in ids)
                if (id == existingId)
                    return true;

            return false;
        }
    }
}