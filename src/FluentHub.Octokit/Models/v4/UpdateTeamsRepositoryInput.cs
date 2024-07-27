// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated input type of UpdateTeamsRepository
	/// </summary>
	public class UpdateTeamsRepositoryInput
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// Repository ID being granted access to.
		/// </summary>
		public ID RepositoryId { get; set; }

		/// <summary>
		/// A list of teams being granted access. Limit: 10
		/// </summary>
		public List<ID> TeamIds { get; set; }

		/// <summary>
		/// Permission that should be granted to the teams.
		/// </summary>
		public RepositoryPermission Permission { get; set; }
	}
}
