// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of MoveProjectCard
	/// </summary>
	public class MoveProjectCardPayload
	{
		/// <summary>
		/// The new edge of the moved card.
		/// </summary>
		public ProjectCardEdge CardEdge { get; set; }

		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }
	}
}
