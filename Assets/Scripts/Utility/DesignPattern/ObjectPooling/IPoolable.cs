using System;

namespace Utility.DesignPattern.ObjectPooling {
	public interface IPoolable {
		void SetRelease(Action release);
	}
}