// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of CreateTeamDiscussion
	/// </summary>
	public class CreateTeamDiscussionPayload
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The new discussion.
		/// </summary>
		[Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
		public TeamDiscussion TeamDiscussion { get; set; }
	}
}
