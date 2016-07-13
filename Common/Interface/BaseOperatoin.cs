using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public class BaseOperatoin<T> where T : new()
    {
        /// <summary>
        /// Clone inner function.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private object Clone<Ti>(Ti target) where Ti : new()
        {
            Ti result = new Ti();
            Type t = target.GetType();
            var typeArr = t.GetProperties();
            var resultArr = result.GetType().GetProperties();
            if (target != null)
            {
                foreach (var pi in typeArr)
                {
                    if (pi.PropertyType == typeof(int)
                        || pi.PropertyType == typeof(string)
                        || pi.PropertyType == typeof(double)
                        || pi.PropertyType == typeof(float)
                        || pi.PropertyType == typeof(byte)
                        || pi.PropertyType == typeof(Int64))
                    {
                        resultArr.FirstOrDefault(x => x.Name == pi.Name).SetValue(result, pi.GetValue(target));
                    }
                    else
                    {
                        resultArr.FirstOrDefault(x => x.Name == pi.Name).SetValue(result, Clone(pi.GetValue(target)));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Return a deep copy of this instance.
        /// </summary>
        /// <returns></returns>
        public T Clone()
        {
            T result = new T();
            Type t = this.GetType();
            var typeArr = t.GetProperties();
            var resultArr = result.GetType().GetProperties();
            if (this != null)
            {
                foreach (var pi in typeArr)
                {
                    if (pi.PropertyType == typeof(int)
                        || pi.PropertyType == typeof(string)
                        || pi.PropertyType == typeof(double)
                        || pi.PropertyType == typeof(float)
                        || pi.PropertyType == typeof(byte)
                        || pi.PropertyType == typeof(Int64))
                    {
                        resultArr.FirstOrDefault(x => x.Name == pi.Name).SetValue(result, pi.GetValue(this));
                    }
                    else
                    {
                        resultArr.FirstOrDefault(x => x.Name == pi.Name).SetValue(result, Clone(pi.GetValue(this)));
                    }
                    /*if (pi.PropertyType == typeof(string))
                    {
                        
                        if (pi.GetValue(input, null) == null)
                        {
                            pi.SetValue(obj, "", null);
                        }
                    }
                    else if (pi.PropertyType.IsGenericType || pi.PropertyType.IsArray || pi.PropertyType.IsClass)
                    {
                        var paras = pi.GetIndexParameters();
                        if (paras.Count() > 0)
                        {
                            int i = 0;
                            tempItem = pi.GetValue(obj, new object[] { 0 });
                            while (tempItem != null)
                            {
                                pi.SetValue(obj, CJRemoveNULLByRecursive(tempItem), new object[] { i });
                                i++;
                                try
                                {
                                    tempItem = pi.GetValue(obj, new object[] { i });
                                }
                                catch (Exception)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            pi.SetValue(obj, CJRemoveNULLByRecursive(pi.GetValue(obj, null)), null);
                        }
                    }*/
                }
            }
            return result;
        }

        /// <summary>
        /// Inner function to judge that whether obj1 and obj2 are equal.
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        private bool IsEqual(object obj1, object obj2)
        {
            bool result = true;
            var typeArr = obj1.GetType().GetProperties();
            var resultArr = obj2.GetType().GetProperties();
            if (obj1.GetType() == typeof(int) || obj1.GetType() == typeof(double) || obj1.GetType() == typeof(string) || obj1.GetType() == typeof(float))
            {
                result = obj1.GetType() == obj2.GetType() && obj1.Equals(obj2);
            }
            else if (obj1 != null)
            {
                for (int i = 0; i < typeArr.Length; i++)
                {
                    if (typeArr[i].PropertyType == typeof(int))
                    {
                        if (resultArr[i].PropertyType != typeof(int) || (int)typeArr[i].GetValue(obj1) != (int)resultArr[i].GetValue(obj2))
                        {
                            result = false;
                            break;
                        }
                    }
                    else if (typeArr[i].PropertyType == typeof(string))
                    {
                        if (resultArr[i].PropertyType != typeof(string) || (string)typeArr[i].GetValue(obj1) != (string)resultArr[i].GetValue(obj2))
                        {
                            result = false;
                            break;
                        }
                    }
                    else if (typeArr[i].PropertyType == typeof(double))
                    {
                        if (resultArr[i].PropertyType != typeof(double) || (double)typeArr[i].GetValue(obj1) != (double)resultArr[i].GetValue(obj2))
                        {
                            result = false;
                            break;
                        }
                    }
                    else if (typeArr[i].PropertyType == typeof(float))
                    {
                        if (resultArr[i].PropertyType != typeof(float) || (float)typeArr[i].GetValue(obj1) != (float)resultArr[i].GetValue(obj2))
                        {
                            result = false;
                            break;
                        }
                    }
                    else if (typeArr[i].PropertyType == typeof(byte))
                    {
                        if (resultArr[i].PropertyType != typeof(byte) || (byte)typeArr[i].GetValue(obj1) != (byte)resultArr[i].GetValue(obj2))
                        {
                            result = false;
                            break;
                        }
                    }
                    else if (typeArr[i].PropertyType == typeof(Int64))
                    {
                        if (resultArr[i].PropertyType != typeof(Int64) || (Int64)typeArr[i].GetValue(obj1) != (Int64)resultArr[i].GetValue(obj2))
                        {
                            result = false;
                            break;
                        }
                    }
                    else
                    {
                        if (resultArr[i].PropertyType != typeArr[i].PropertyType || !IsEqual(typeArr[i].GetValue(obj1), resultArr[i].GetValue(obj2)))
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private bool IsArrayEqual<T1>(T1[] array1, T1[] array2)
        {
            bool result = true;
            if (array1.Length != array2.Length)
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < array1.Length; i++)
                {
                    if (!IsEqual(array1[i], array2[i]))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Is the target equal to this instance.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool IsEqual(T target)
        {
            bool result = true;
            var typeArr = this.GetType().GetProperties();
            var resultArr = target.GetType().GetProperties();
            if (this.GetType() == typeof(int) || this.GetType() == typeof(double) || this.GetType() == typeof(string) || this.GetType() == typeof(float))
            {
                result = this.GetType() == target.GetType() && (object)target == this;
            }
            else if (this != null)
            {
                for (int i = 0; i < typeArr.Length; i++)
                {
                    if (typeArr[i].GetMethod.Name != "get_Item")//It will throw exception while the class got method such as "public double this[int colum, int line]"
                    {
                        if (typeArr[i].PropertyType == typeof(int))
                        {
                            if (resultArr[i].PropertyType != typeof(int) || (int)typeArr[i].GetValue(this) != (int)resultArr[i].GetValue(target))
                            {
                                result = false;
                                break;
                            }
                        }
                        else if (typeArr[i].PropertyType == typeof(string))
                        {
                            if (resultArr[i].PropertyType != typeof(string) || (string)typeArr[i].GetValue(this) != (string)resultArr[i].GetValue(target))
                            {
                                result = false;
                                break;
                            }
                        }
                        else if (typeArr[i].PropertyType == typeof(double))
                        {
                            if (resultArr[i].PropertyType != typeof(double) || (double)typeArr[i].GetValue(this) != (double)resultArr[i].GetValue(target))
                            {
                                result = false;
                                break;
                            }
                        }
                        else if (typeArr[i].PropertyType == typeof(double[]))
                        {
                            if (resultArr[i].PropertyType != typeof(double[]) || !IsArrayEqual<double>((double[]) typeArr[i].GetValue(this), (double[]) resultArr[i].GetValue(target)))
                            {
                                result = false;
                                break;
                            }
                        }
                        else if (typeArr[i].PropertyType == typeof(float))
                        {
                            if (resultArr[i].PropertyType != typeof(float) || (float)typeArr[i].GetValue(this) != (float)resultArr[i].GetValue(target))
                            {
                                result = false;
                                break;
                            }
                        }
                        else if (typeArr[i].PropertyType == typeof(byte))
                        {
                            if (resultArr[i].PropertyType != typeof(byte) || (byte)typeArr[i].GetValue(this) != (byte)resultArr[i].GetValue(target))
                            {
                                result = false;
                                break;
                            }
                        }
                        else if (typeArr[i].PropertyType == typeof(Int64))
                        {
                            if (resultArr[i].PropertyType != typeof(Int64) || (Int64)typeArr[i].GetValue(this) != (Int64)resultArr[i].GetValue(target))
                            {
                                result = false;
                                break;
                            }
                        }
                        else
                        {
                            if (resultArr[i].PropertyType != typeArr[i].PropertyType || !IsEqual(typeArr[i].GetValue(this), resultArr[i].GetValue(target)))
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
