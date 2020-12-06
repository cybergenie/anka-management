using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace EFCoreTest.TableAdapter
{
    public class DataAdapter
    {
        private DataTable _collectionView = null;
        public DataTable CollectionView => _collectionView;
        public object GetDataList<T>(List<T> data, string item)
        {
            object collectionView = null;
            var dataTable = DbTools.ToDataTable(data);
            DataTable dataCoventer = null;
            switch (item)
            {

                case "BasicInfo":
                    {
                        dataCoventer = DbTools.BasicinfoConverter(dataTable);
                        _collectionView = dataCoventer;
                        var list = DbTools.ToDataList<BasicInfoTable>(dataCoventer);
                        collectionView = new ObservableCollection<BasicInfoTable>(list);
                    }
                    break;
                case "Exercise":
                    {
                        dataCoventer = DbTools.ExerciseConverter(dataTable);
                        _collectionView = dataCoventer;
                        var list = DbTools.ToDataList<ExerciseTable>(dataCoventer);
                        collectionView = new ObservableCollection<ExerciseTable>(list);
                    }
                    break;
                case "GAD":
                    {
                        dataCoventer = DbTools.GADConverter(dataTable);
                        _collectionView = dataCoventer;
                        var list = DbTools.ToDataList<GADTable>(dataCoventer);
                        collectionView = new ObservableCollection<GADTable>(list);
                    }
                    break;
                case "IPAQ":
                    {
                        dataCoventer = DbTools.IPAQConverter(dataTable);
                        _collectionView = dataCoventer;
                        var list = DbTools.ToDataList<IPAQTable>(dataCoventer);
                        collectionView = new ObservableCollection<IPAQTable>(list);
                    }
                    break;
                case "OHQ":
                    {
                        dataCoventer = DbTools.OHQConverter(dataTable);
                        _collectionView = dataCoventer;
                        var list = DbTools.ToDataList<OHQTable>(dataCoventer);
                        collectionView = new ObservableCollection<OHQTable>(list);
                    }
                    break;
                case "PHQ":
                    {
                        dataCoventer = DbTools.PHQConverter(dataTable);
                        _collectionView = dataCoventer;
                        var list = DbTools.ToDataList<PHQTable>(dataCoventer);
                        collectionView = new ObservableCollection<PHQTable>(list);
                    }
                    break;
                default: break;
            }
            return collectionView;
        }
        
    }
}
