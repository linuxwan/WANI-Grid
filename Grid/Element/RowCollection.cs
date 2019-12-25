using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 이 소스는 LGPL(GNU Lesser General Public Licence)를 따릅니다.
/// 이 컨트롤을 사용하는 데에는 제한이 없으나, 해당 소스를 변경하거나 개선했을 경우에는 LGPL에 의해 소스를 제공해야 합니다.
/// 이 소스는 누구나 자유롭게 사용할 수 있고, 배포할 수 있습니다.
/// GitHub : https://github.com/linuxwan/WANI-Grid
/// Blog : https://uniworks.tistory.com
/// 작성자 : Park Chungwan
/// </summary>
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
