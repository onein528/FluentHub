// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of TransferEnterpriseOrganization
	/// </summary>
	public class TransferEnterpriseOrganizationPayload
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The organization for which a transfer was initiated.
		/// </summary>
		public Organization Organization { get; set; }
	}
}
