using OneOf;

namespace Tracky.Domain.Common;

[GenerateOneOf]
public partial class Result<T> : OneOfBase<T, DomainError>;
