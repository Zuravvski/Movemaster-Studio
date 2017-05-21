using IDE.Common.Models;
using System.Collections.ObjectModel;
using Driver;
// <copyright file="EditorViewModelTest.cs">Copyright ©  2017</copyright>

using System;
using IDE.Common.ViewModels;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDE.Common.ViewModels.Tests
{
    /// <summary>This class contains parameterized unit tests for EditorViewModel</summary>
    [TestClass]
    [PexClass(typeof(EditorViewModel))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class EditorViewModelTest
    {

        /// <summary>Test stub for .ctor()</summary>
        [PexMethod]
        public EditorViewModel ConstructorTest()
        {
            EditorViewModel target = new EditorViewModel();
            return target;
            // TODO: add assertions to method EditorViewModelTest.ConstructorTest()
        }

        /// <summary>Test stub for get_Appearance()</summary>
        [PexMethod]
        public AppearanceViewModel AppearanceGetTest([PexAssumeUnderTest]EditorViewModel target)
        {
            AppearanceViewModel result = target.Appearance;
            return result;
            // TODO: add assertions to method EditorViewModelTest.AppearanceGetTest(EditorViewModel)
        }

        /// <summary>Test stub for get_IntellisenseIsChecked()</summary>
        [PexMethod]
        public bool IntellisenseIsCheckedGetTest([PexAssumeUnderTest]EditorViewModel target)
        {
            bool result = target.IntellisenseIsChecked;
            return result;
            // TODO: add assertions to method EditorViewModelTest.IntellisenseIsCheckedGetTest(EditorViewModel)
        }

        /// <summary>Test stub for get_ProgramList()</summary>
        [PexMethod]
        public ObservableCollection<Program> ProgramListGetTest([PexAssumeUnderTest]EditorViewModel target)
        {
            ObservableCollection<Program> result = target.ProgramList;
            return result;
            // TODO: add assertions to method EditorViewModelTest.ProgramListGetTest(EditorViewModel)
        }

        /// <summary>Test stub for get_SelectedTabItem()</summary>
        [PexMethod]
        public TabItem SelectedTabItemGetTest([PexAssumeUnderTest]EditorViewModel target)
        {
            TabItem result = target.SelectedTabItem;
            return result;
            // TODO: add assertions to method EditorViewModelTest.SelectedTabItemGetTest(EditorViewModel)
        }

        /// <summary>Test stub for get_SyntaxIsChecked()</summary>
        [PexMethod]
        public bool SyntaxIsCheckedGetTest([PexAssumeUnderTest]EditorViewModel target)
        {
            bool result = target.SyntaxIsChecked;
            return result;
            // TODO: add assertions to method EditorViewModelTest.SyntaxIsCheckedGetTest(EditorViewModel)
        }

        /// <summary>Test stub for get_TabItems()</summary>
        [PexMethod]
        public ObservableCollection<TabItem> TabItemsGetTest([PexAssumeUnderTest]EditorViewModel target)
        {
            ObservableCollection<TabItem> result = target.TabItems;
            return result;
            // TODO: add assertions to method EditorViewModelTest.TabItemsGetTest(EditorViewModel)
        }

        /// <summary>Test stub for set_IntellisenseIsChecked(Boolean)</summary>
        [PexMethod]
        public void IntellisenseIsCheckedSetTest([PexAssumeUnderTest]EditorViewModel target, bool value)
        {
            target.IntellisenseIsChecked = value;
            // TODO: add assertions to method EditorViewModelTest.IntellisenseIsCheckedSetTest(EditorViewModel, Boolean)
        }

        /// <summary>Test stub for set_ProgramList(ObservableCollection`1&lt;Program&gt;)</summary>
        [PexMethod]
        public void ProgramListSetTest([PexAssumeUnderTest]EditorViewModel target, ObservableCollection<Program> value)
        {
            target.ProgramList = value;
            // TODO: add assertions to method EditorViewModelTest.ProgramListSetTest(EditorViewModel, ObservableCollection`1<Program>)
        }

        /// <summary>Test stub for set_SelectedTabItem(TabItem)</summary>
        [PexMethod]
        public void SelectedTabItemSetTest([PexAssumeUnderTest]EditorViewModel target, TabItem value)
        {
            target.SelectedTabItem = value;
            // TODO: add assertions to method EditorViewModelTest.SelectedTabItemSetTest(EditorViewModel, TabItem)
        }

        /// <summary>Test stub for set_SyntaxIsChecked(Boolean)</summary>
        [PexMethod]
        public void SyntaxIsCheckedSetTest([PexAssumeUnderTest]EditorViewModel target, bool value)
        {
            target.SyntaxIsChecked = value;
            // TODO: add assertions to method EditorViewModelTest.SyntaxIsCheckedSetTest(EditorViewModel, Boolean)
        }

        /// <summary>Test stub for set_TabItems(ObservableCollection`1&lt;TabItem&gt;)</summary>
        [PexMethod]
        public void TabItemsSetTest([PexAssumeUnderTest]EditorViewModel target, ObservableCollection<TabItem> value)
        {
            target.TabItems = value;
            // TODO: add assertions to method EditorViewModelTest.TabItemsSetTest(EditorViewModel, ObservableCollection`1<TabItem>)
        }
    }
}
