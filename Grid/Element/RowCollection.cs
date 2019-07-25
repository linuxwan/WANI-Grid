using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WANI_Grid.Grid.Element
{
    public class RowCollection: CollectionBase
    {
        /// <summary>
        /// index 위치의 Row 정보를 설정하거나 가져온다.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Row this[int index]
        {
            get
            {
                if (index >= 0 && index < this.Count)
                    return List[index] as Row;
                else
                    return null;
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// Row 정보가 존재하는 Index를 반환한다.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int this[Row item]
        {
            get { return List.IndexOf(item); }
        }

        /// <summary>
        /// Row를 추가하고 추가한 Row의 Index를 반환
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual int Add(Row item)
        {
            int index = List.Add(item);
            return index;
        }

        /// <summary>
        /// 여러 개의 Row를 추가한다.
        /// </summary>
        /// <param name="items"></param>
        public virtual void AddRange(Row[] items)
        {
            lock(List.SyncRoot)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    List.Add(items[i]);
                }
            }
        }

        /// <summary>
        /// Row를 제거한다.
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(Row item)
        {
            List.Remove(item);
        }

        /// <summary>
        /// 모든 Row를 지운다.
        /// </summary>
        public new void Clear()
        {
            List.Clear();
        }

        /// <summary>
        /// Row의 Index를 반환
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual int IndexOf(Row item)
        {
            return List.IndexOf(item);
        }

        /// <summary>
        /// 특정 Index 위치에 Row를 Insert 한다.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public virtual void Insert(int index, Row item)
        {
            List.Insert(index, item);
        }
    }
}
