// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated input type of ReopenPullRequest
	/// </summary>
	public class ReopenPullRequestInput
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// ID of the pull request to be reopened.
		/// </summary>
		public ID PullRequestId { get; set; }
	}
}
