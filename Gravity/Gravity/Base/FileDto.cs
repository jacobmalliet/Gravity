﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.Base
{
	public abstract class FileDto
	{
		internal FileDto() { }

		internal string LastOperationMD5 { get; set; }

		internal string GetCurrentMD5()
		{
			using (var md5 = MD5.Create())
			{
				byte[] byteHashedPassword = md5.ComputeHash(GetStream());
				return BitConverter.ToString(byteHashedPassword).Replace("-", "").ToLower();
			}
		}

		protected abstract Stream GetStream();
	}

	public class FilePathFileDto : FileDto
	{
		public string FilePath { get; set; }

		public ByteArrayFileDto StoreInMemory()
		{
			return new ByteArrayFileDto
			{
				ByteArray = File.ReadAllBytes(this.FilePath),
				LastOperationMD5 = this.LastOperationMD5,
				FileName = Path.GetFileName(this.FilePath)
			};
		}

		protected override Stream GetStream() => File.OpenRead(FilePath);
	}

	public class ByteArrayFileDto : FileDto
	{
		public byte[] ByteArray { get; set; }

		public string FileName { get; set; }

		public FilePathFileDto WriteToFile(string filePath)
		{
			File.WriteAllBytes(filePath, this.ByteArray);
			return new FilePathFileDto
			{
				FilePath = filePath,
				LastOperationMD5 = this.LastOperationMD5
			};
		}

		protected override Stream GetStream() => new MemoryStream(ByteArray);
	}
}
