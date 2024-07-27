// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of UpdateIpAllowListEnabledSetting
	/// </summary>
	public class UpdateIpAllowListEnabledSettingPayload
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The IP allow list owner on which the setting was updated.
		/// </summary>
		public IpAllowListOwner Owner { get; set; }
	}
}
