public class DisplayedError
{
	public bool energyError = false;
	public bool ventilationError = false;

	public DisplayedError(bool energyError, bool ventilationError) {
		this.energyError = energyError;
		this.ventilationError = ventilationError;
	}
}

