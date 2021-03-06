﻿using System;
using System.IO;

namespace Tests
{
    /// <summary>
    /// A disposable KSP instance. Use the `.KSP` property to access, will
    /// be automatically cleaned up on DisposableKSP falling out of using() scope.
    /// </summary>
    public class DisposableKSP :IDisposable
    {
        private readonly string good_ksp = Tests.TestData.good_ksp_dir();
        private CKAN.KSP _ksp;
        private string disposable_dir;

        public CKAN.KSP KSP {
            get
            {
                return _ksp;
            }
        }

        /// <summary>
        /// Creates a copy of the provided argument, or a known-good KSP install if passed null.
        /// Use .KSP to access the KSP object itself.
        /// </summary>
        public DisposableKSP(string directory_to_clone = null)
        {
            directory_to_clone = directory_to_clone ?? good_ksp;
            disposable_dir = Tests.TestData.NewTempDir();
            Tests.TestData.CopyDirectory(directory_to_clone, disposable_dir);
            _ksp = new CKAN.KSP(disposable_dir);
        }

        public void Dispose()
        {
            Directory.Delete(disposable_dir, true);
            _ksp = null; // In case .Dispose() was called manually.
        }
    }
}

