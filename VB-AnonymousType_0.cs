using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[CompilerGenerated]
[DebuggerDisplay("\\{ $0 = {_item0}, $1 = {_item1}, $2 = {_item2} }", Type = "<anonymous type>")]
internal sealed class VB_0024AnonymousType_0<T0, T1, T2>
{
	private readonly T0 _item0;
	private readonly T1 _item1;
	private readonly T2 _item2;

	public VB_0024AnonymousType_0(T0 item0, T1 item1, T2 item2)
	{
		_item0 = item0;
		_item1 = item1;
		_item2 = item2;
	}

	public override bool Equals(object obj)
	{
		var other = obj as VB_0024AnonymousType_0<T0, T1, T2>;
		return other != null &&
		       EqualityComparer<T0>.Default.Equals(_item0, other._item0) &&
		       EqualityComparer<T1>.Default.Equals(_item1, other._item1) &&
		       EqualityComparer<T2>.Default.Equals(_item2, other._item2);
	}

	public override int GetHashCode()
	{
		return (EqualityComparer<T0>.Default.GetHashCode(_item0) * -1521134295 +
		        EqualityComparer<T1>.Default.GetHashCode(_item1)) * -1521134295 +
		       EqualityComparer<T2>.Default.GetHashCode(_item2);
	}

	public override string ToString()
	{
		return string.Format("{{ $0 = {0}, $1 = {1}, $2 = {2} }}", _item0, _item1, _item2);
	}
}
