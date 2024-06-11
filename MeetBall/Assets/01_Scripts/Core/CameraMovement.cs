using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public enum CamPos
{
	Left, Front, Right, Back, Up, Down
}

public class CameraMovement : MonoSingleton<CameraMovement>
{
	public List<Transform> transforms = new List<Transform>();
	public CinemachineVirtualCamera[] cinemachineCam;

	private CinemachineVirtualCamera previousCam;
	public Transform curTransfrom;

	public HoverButton button { get; set; }
	public Direction _dir;

	private CamPos camPos;

	public int left, up;

    private List<Item> items;


	private void Start()
	{
		previousCam = cinemachineCam[3];
		camPos = CamPos.Back;
	}

	public void FindItems()
	{
		items = GameManager.Instance.FindAllItems();
	}

	public void CameraReset()
	{
		cinemachineCam[3].Priority = 10;
		camPos = CamPos.Back;
		previousCam.Priority = 5;
	}

	public void ChangeCamera(Direction dir)
	{
		FindMovement();

		foreach (var c in cinemachineCam)
		{
			if (c.Priority == 10)
				previousCam = c;
		}

		previousCam.Priority = 5;

		switch (dir)
		{
			case Direction.Left:
				{
					left++;
					int i = left % 4;
					if (i == 0) i = 4;
					curTransfrom = cinemachineCam[i - 1].transform;
					cinemachineCam[i - 1].Priority = 10;
					camPos = (CamPos)i - 1;
				}
				break;
			case Direction.Right:
				{
					left--;
					if (left == -1) left = 3;
					int i = left % 4;
					if (i == 0) i = 4;
					curTransfrom = cinemachineCam[i - 1].transform;
					cinemachineCam[i - 1].Priority = 10;
					camPos = (CamPos)i - 1;
				}
				break;
			case Direction.Up:
				{
					if (up < 0)
					{
						up = 0;
						int i = left % 4;
						if (i == 0) i = 4;
						curTransfrom = cinemachineCam[i - 1].transform;
						cinemachineCam[i - 1].Priority = 10;
						camPos = (CamPos)i - 1;
					}
					else
					{
						up++;

						curTransfrom = cinemachineCam[4].transform;
						cinemachineCam[4].Priority = 10;
						camPos = CamPos.Up;
					}
				}
				break;
			case Direction.Down:
				{
					if (up > 0)
					{
						up = 0;
						int i = left % 4;
						if (i == 0) i = 4;
						curTransfrom = cinemachineCam[i - 1].transform;
						cinemachineCam[i - 1].Priority = 10;
						camPos = (CamPos)i - 1;
					}
					else
					{
						up--;

						curTransfrom = cinemachineCam[5].transform;
						cinemachineCam[5].Priority = 10;
						camPos = CamPos.Down;
					}
				}
				break;
		}

		ItemRot(curTransfrom == cinemachineCam[5].transform);

		up = Mathf.Clamp(up, -1, 1);
		_dir = dir;
	}

	public void ChangeUpDown(CamPos cp)
	{
		print(cp);
		switch (cp)
		{
			case CamPos.Front:
				{
					cinemachineCam[4].transform.Rotate(new Vector3(90, 180, 0));
					cinemachineCam[5].transform.Rotate(new Vector3(-90, 180, 0));
				}
				break;
			case CamPos.Back:
				{
					cinemachineCam[4].transform.Rotate(new Vector3(90, 0, 0));
					cinemachineCam[5].transform.Rotate(new Vector3(-90, 0, 0));
				}
				break;
			default:
				{
					cinemachineCam[4].transform.Rotate(new Vector3(90, 0, 0));
					cinemachineCam[5].transform.Rotate(new Vector3(-90, 0, 0));
				}
				break;
		}
	}

    private void ItemRot(bool value)
    {
        foreach (Item item in items)
        {
            item.Rotation(value);
        }
    }


	private void FindMovement()
	{
		Movement[] movement = FindObjectsOfType<Movement>();

		foreach (Movement m in movement)
			m.RayCheck();
	}
}