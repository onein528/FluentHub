// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated input type of LinkProjectV2ToTeam
	/// </summary>
	public class LinkProjectV2ToTeamInput
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The ID of the project to link to the team.
		/// </summary>
		public ID ProjectId { get; set; }

		/// <summary>
		/// The ID of the team to link to the project.
		/// </summary>
		public ID TeamId { get; set; }
	}
}
